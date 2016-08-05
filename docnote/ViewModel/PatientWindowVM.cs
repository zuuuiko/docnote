using docnote.Model;
using docnote.Model.Documents;
using docnote.Resources;
using docnote.View;
using docnote.View.Documents;
using docnote.ViewModel.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace docnote.ViewModel
{
    public class PatientWindowVM : ViewModelBase
    {
        private readonly IDataService _dataService;
        PeriodsRadioButtons _periodsRadioButtons;
        public PeriodsRadioButtons PeriodsRadioButtons
        {
            get
            {
                return _periodsRadioButtons;
            }
            set
            {
                Set(ref _periodsRadioButtons, value);
            }
        }

        MainViewModel _mainVM;

        private Patient _patient;
        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                Set(ref _patient, value);
            }
        }

        private Document _selectedDocument;
        private ObservableCollection<Document> _documents;
        public Document SelectedDocument
        {
            get { return _selectedDocument; }
            set { Set(ref _selectedDocument, value); }
        }
        public ObservableCollection<Document> Documents
        {
            get { return _documents; }
            set { Set(ref _documents, value); }
        }

        private Document _selectedDocumentForm;
        private ObservableCollection<Document> _documentFormList;
        public Document SelectedDocumentForm
        {
            get { return _selectedDocumentForm; }
            set { Set(ref _selectedDocumentForm, value); }
        }
        public ObservableCollection<Document> DocumentFormList
        {
            get { return _documentFormList; }
            set { Set(ref _documentFormList, value); }
        }

        private CardEntry _selectedCardEntry;
        private ObservableCollection<CardEntry> _cardEntries;
        public CardEntry SelectedCardEntry
        {
            get
            {
                return _selectedCardEntry;
            }
            set
            {
                Set(ref _selectedCardEntry, value);
            }
        }
        public ObservableCollection<CardEntry> CardEntries
        {
            get
            {
                return _cardEntries;
            }
            set
            {
                Set(ref _cardEntries, value);
            }
        }

        public ICommand ShowCardEntriesCommand { get; private set; }
        public ICommand CardEntryDoubleClickCommand { get; private set; }
        public ICommand AddCardEntryClickCommand { get; private set; }
        public ICommand DeleteCardEntryClickCommand { get; private set; }
        public ICommand SaveAndClosePatientClickCommand { get; private set; }
        public ICommand ClosePatientClickCommand { get; private set; }
        public ICommand OpenDocumentsFlyoutCommand { get; private set; }
        public ICommand CreateOpenDocumentCommand { get; private set; }

        [PreferredConstructor]
        public PatientWindowVM(MainViewModel mainVM, IDataService dataService)
        {
            _dataService = dataService;
            Init(mainVM);
            Patient = new Patient { Address = new Address(), Card = new Card() };
        }

        public PatientWindowVM(MainViewModel mainVM, Patient p, IDataService dataService)
        {
            _dataService = dataService;
            Patient = p;
            LoadDocuments();
            LoadAddress();
            LoadCard();
            LoadCardEntries();
            Init(mainVM);  
        }


        private void Init(MainViewModel mainVM)
        {
            _mainVM = mainVM;
            PeriodsRadioButtons = PeriodsRadioButtons.All;
            ShowCardEntriesCommand = new RelayCommand<PeriodsRadioButtons>(ShowCardEntries);
            CardEntryDoubleClickCommand = new RelayCommand<CardEntry>(OpenCardEntryWindow);
            AddCardEntryClickCommand = new RelayCommand(OpenCardEntryWindow);
            DeleteCardEntryClickCommand = new RelayCommand<CardEntry>(DeleteCardEntry);
            SaveAndClosePatientClickCommand = new RelayCommand(SaveAndClosePatient);
            ClosePatientClickCommand = new RelayCommand(ClosePatientWindow);
            OpenDocumentsFlyoutCommand = new RelayCommand<Flyout>(OpenDocumentsFlyout);
            DocumentFormList = new ObservableCollection<Document> { new Form_063_o(), new Form_063_o() };
            CreateOpenDocumentCommand = new RelayCommand<Document>(CreateOpenDocument);

        }

        private void CreateOpenDocument(Document doc)
        {
            if (Patient.Id == 0)
            {
                SavePatient();
                _mainVM.LoadPatients();
            }
            //TODO: switch types of docs
            Form_063_o_Window fw = new Form_063_o_Window();
            fw.DataContext = new Form_063_o_VM(this, doc, _dataService);
            fw.Show();
            //System.Diagnostics.Debug.WriteLine(
            //    $"DocumentName - {doc.DocumentName}, Patient.LastName ");
            //cew.DataContext = new CardEntryWindowVM(this, Patient.Card, _dataService);
            //cew.ShowDialog();
        }

        private void OpenDocumentsFlyout(Flyout flyout)
        {
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private async void DeleteCardEntry(CardEntry ce)
        {
            if (ce == null) return;

            var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            if (window != null)
            {
                var result = await window.ShowMessageAsync("Видалити?", $"{ce.CreationDate}",
                                                                           MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;
            }

            _dataService.DeleteCardEntry(
                async (isDeleted, error) =>
                {
                    if (window != null && isDeleted)
                        await window.ShowMessageAsync(null, $"{ce.CreationDate} видалений");
                }, ce);
            LoadCardEntries();
        }

        private void ClosePatientWindow()
        {
            Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault().Close();
        }

        private void SaveAndClosePatient()
        {
            _dataService.AddUpdatePatient(
                async (isSaved, error) =>
                {
                    var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : error.Message);
                        if (result == MessageDialogResult.Affirmative)
                        {
                            _mainVM.LoadPatients();
                            ClosePatientWindow();
                        }
                    }
                }, Patient);
        }

        private void SavePatient()
        {
            _dataService.AddUpdatePatient(
               (isSaved, error) =>
                {
                    
                }, Patient);
        }

        private void OpenCardEntryWindow(CardEntry ce)
        {
            CardEntryWindow pw = new CardEntryWindow();
            pw.DataContext = new CardEntryWindowVM(this, ce, _dataService);
            pw.ShowDialog();
        }

        //New CardEntry
        private void OpenCardEntryWindow()
        {
            if (Patient.Id == 0)
            {
                SavePatient();
                _mainVM.LoadPatients();
            }

            CardEntryWindow cew = new CardEntryWindow();
            //TODO: delete Patient.Card
            cew.DataContext = new CardEntryWindowVM(this, Patient.Card, _dataService);
            cew.ShowDialog();
        }

        private void ShowCardEntries(PeriodsRadioButtons period)
        {
            DateTime lastMonthAgo;
            switch (period)
            {
                case PeriodsRadioButtons.All:
                    LoadCardEntries();
                    break;
                case PeriodsRadioButtons.LastMonth:
                    lastMonthAgo = DateTime.Today.AddMonths(-1);
                    LoadCardEntriesByPeriod(lastMonthAgo);
                    break;
                case PeriodsRadioButtons.Last3Month:
                    lastMonthAgo = DateTime.Today.AddMonths(-3);
                    LoadCardEntriesByPeriod(lastMonthAgo);
                    break;
                default:
                    break;
            }
        }

        private void LoadAddress()
        {
            _dataService.GetAddress(
                (address, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Patient.Address = address;
                }, Patient);
        }

        private void LoadCard()
        {
            _dataService.GetCard(
                (card, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Patient.Card = card;
                }, Patient);
        }

        private void LoadDocuments()
        {
            _dataService.GetDocumentsAsync(
                (documents, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Documents = documents;
                }, Patient);
        }

        public void LoadCardEntries()
        {
            _dataService.GetCardEntriesAsync(
                (cardEntries, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient.Card);
        }

        private void LoadCardEntriesByPeriod(DateTime earliestDate, DateTime latestDate)
        {
            _dataService.GetCardEntriesByPeriodAsync(
                (cardEntries, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient.Card, earliestDate, latestDate);
        }

        private void LoadCardEntriesByPeriod(DateTime earliestDate)
        {
            _dataService.GetCardEntriesByPeriodAsync(
                (cardEntries, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient.Card, earliestDate);
        }

        //public override void Cleanup()
        //{
        //    // Clean up if needed

        //    base.Cleanup();
        //}
    }
}
