using docnote.Model;
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
using System.Windows.Controls;
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
        Action _updatePatientsDataGrid;
       // MainViewModel _mainVM;

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

        private ObservableCollection<InvalidDisease> _invalidDiseases;
        public ObservableCollection<InvalidDisease> InvalidDiseases
        {
            get
            {
                return _invalidDiseases;
            }
            set
            {
                Set(ref _invalidDiseases, value);
            }
        }

        private ObservableCollection<DispDisease> _dispDiseases;
        public ObservableCollection<DispDisease> DispDiseases
        {
            get
            {
                return _dispDiseases;
            }
            set
            {
                Set(ref _dispDiseases, value);
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
        public ICommand DeleteDocumentClickCommand { get; private set; }
        public ICommand InvalidDiseaseDoubleClickCommand { get; private set; }
        public ICommand DeleteInvalidDiseaseClickCommand { get; private set; }
        public ICommand DispDiseaseDoubleClickCommand { get; private set; }
        public ICommand DeleteDispDiseaseClickCommand { get; private set; }

        [PreferredConstructor]
        public PatientWindowVM(Action reloadPatients, IDataService dataService)
        {
            _dataService = dataService;
            Init(reloadPatients);
            Patient = new Patient { Address = new Address(), Card = new Card(), Documents = new HashSet<Document>() };
        }

        public PatientWindowVM(Action reloadPatients, Patient p, IDataService dataService)
        {
            GC.Collect();
            _dataService = dataService;
            Patient = p;
            LoadDocuments();
            //LoadAddress();
            LoadCard();
            LoadCardEntries();
            Init(reloadPatients);
        }

        private void Init(Action reloadPatients)
        {
            //_mainVM = mainVM;
            _updatePatientsDataGrid = reloadPatients;
            InitInvalidDiseases();
            InitDispDiseases();
            PeriodsRadioButtons = PeriodsRadioButtons.All;
            ShowCardEntriesCommand = new RelayCommand<PeriodsRadioButtons>(ShowCardEntries);
            CardEntryDoubleClickCommand = new RelayCommand<CardEntry>(OpenCardEntryWindow);
            AddCardEntryClickCommand = new RelayCommand(OpenCardEntryWindow);
            DeleteCardEntryClickCommand = new RelayCommand<CardEntry>(DeleteCardEntry);
            SaveAndClosePatientClickCommand = new RelayCommand(SaveAndClosePatient);
            ClosePatientClickCommand = new RelayCommand(ClosePatientWindow);
            OpenDocumentsFlyoutCommand = new RelayCommand<Flyout>(OpenDocumentsFlyout);
            DocumentFormList = new ObservableCollection<Document> { new Form_025_6_o(), new Form_063_o() };
            CreateOpenDocumentCommand = new RelayCommand<Document>(CreateOpenDocument);
            DeleteDocumentClickCommand = new RelayCommand<Document>(DeleteDocument);
            InvalidDiseaseDoubleClickCommand = new RelayCommand<TreeViewItem>(AddDiseaseToInvalidDiseases);
            DeleteInvalidDiseaseClickCommand = new RelayCommand<InvalidDisease>(DeleteInvalidDisease);
            DispDiseaseDoubleClickCommand = new RelayCommand<TreeViewItem>(AddDiseaseToDispDiseases);
            DeleteDispDiseaseClickCommand = new RelayCommand<DispDisease>(DeleteDispDisease);
        }

        private void DeleteInvalidDisease(InvalidDisease obj)
        {
            InvalidDiseases.Remove(obj);
        }

        private void AddDiseaseToInvalidDiseases(TreeViewItem obj)
        {
            var str = obj.Header.ToString();
            var pos = str.IndexOf(' ');
            var disease = new InvalidDisease { Code = str.Substring(0, pos), Name = str.Substring(pos + 1) };
            if (!InvalidDiseases.Any(d => d.Equals(disease)))
            {
                InvalidDiseases.Add(disease);
            }
        }

        private void DeleteDispDisease(DispDisease obj)
        {
            DispDiseases.Remove(obj);
        }

        private void AddDiseaseToDispDiseases(TreeViewItem obj)
        {
            var str = obj.Header.ToString();
            var pos = str.IndexOf(' ');
            var disease = new DispDisease { Code = str.Substring(0, pos), Name = str.Substring(pos + 1) };
            if (!DispDiseases.Any(d => d.Equals(disease)))
            {
                DispDiseases.Add(disease);
            }
        }

        private async void DeleteDocument(Document d)
        {
            if (d == null) return;

            var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            if (window != null)
            {
                var result = await window.ShowMessageAsync("Видалити?", $"{d.DocumentName} {d.CreationDate}",
                                                                           MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;
            }

            _dataService.DeleteDocument(
                async (isDeleted, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    if (window != null && isDeleted)
                        await window.ShowMessageAsync(null, $"{d.DocumentName} {d.CreationDate} видалений");
                }, d);
            LoadDocuments();
        }

        private void CreateOpenDocument(Document doc)
        {
            if (SelectedDocumentForm == null && doc.Id == 0) return;
            if (Patient.Id == 0)
            {
                SavePatient();
                _updatePatientsDataGrid();
            }
            doc.Patient = Patient;
            MetroWindow fw = null;
            switch (doc.DocumentName)
            {
                case @"Форма 025-6/o":
                    fw = new Form_025_6_o_Window();
                    fw.DataContext = new Form_025_6_o_VM(doc, CardEntries.ToList(), _dataService, LoadDocuments);
                    break;
                case @"Форма 063/о":
                    fw = new Form_063_o_Window();
                    fw.DataContext = new Form_063_o_VM(doc, _dataService, LoadDocuments);
                    break;
                default:
                    break;
            }
            fw?.ShowDialog();
            SelectedDocumentForm = null;
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
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
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
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    SaveDiseases(); //!!! not return if error
                    var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : ""); //TODO:
                        if (result == MessageDialogResult.Affirmative)
                        {
                            _updatePatientsDataGrid();
                            ClosePatientWindow();
                        }
                    }
                }, Patient);
        }
        private void SaveDiseases()
        {
            _dataService.AddInvalidDiseases((isSaved, error) =>
            {
                if (error != null)
                {
                    MessageBox.Show(error.StackTrace);
                    return;
                }
            }, InvalidDiseases.ToList(), Patient.Card.CardPatientId);

            _dataService.AddDispDiseases((isSaved, error) =>
            {
                if (error != null)
                {
                    MessageBox.Show(error.StackTrace);
                    return;
                }
            }, DispDiseases.ToList(), Patient.Card.CardPatientId);
        }

        private void SavePatient()
        {
            _dataService.AddUpdatePatient(
               (isSaved, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                }, Patient);
            SaveDiseases();
        }

        private void OpenCardEntryWindow(CardEntry ce)
        {
            CardEntryWindow cew = new CardEntryWindow();
            ce.Card = Patient.Card;
            cew.DataContext = new CardEntryWindowVM(LoadCardEntries, ce, _dataService);
            cew.ShowDialog();
        }

        //New CardEntry
        private void OpenCardEntryWindow()
        {
            if (Patient.Id == 0)
            {
                SavePatient();
                _updatePatientsDataGrid();
            }

            CardEntryWindow cew = new CardEntryWindow();
            CardEntry ce = new CardEntry { CardId = Patient.Card.CardPatientId };
            cew.DataContext = new CardEntryWindowVM(LoadCardEntries, ce, _dataService);
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

        //private void LoadAddress()
        //{
        //    _dataService.GetAddress(
        //        (address, error) =>
        //        {
        //            if (error != null)
        //            {
        //                MessageBox.Show(error.StackTrace);
        //                return;
        //            }
        //            Patient.Address = address;
        //        }, Patient);
        //}

        private void LoadCard()
        {
            _dataService.GetCard(
                (card, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
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
                        MessageBox.Show(error.StackTrace);
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
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient?.Card);
        }

        private void LoadCardEntriesByPeriod(DateTime earliestDate, DateTime latestDate)
        {
            _dataService.GetCardEntriesByPeriodAsync(
                (cardEntries, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient?.Card, earliestDate, latestDate);
        }

        private void LoadCardEntriesByPeriod(DateTime earliestDate)
        {
            _dataService.GetCardEntriesByPeriodAsync(
                (cardEntries, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    CardEntries = cardEntries;
                }, Patient?.Card, earliestDate);
        }

        public void InitInvalidDiseases()
        {
            _dataService.GetInvalidDiseasesAsync(
                (diseases, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    InvalidDiseases = new ObservableCollection<InvalidDisease>(diseases);
                    //Patient.Card.Diseases = diseases;
                }, Patient?.Card);
        }

        public void InitDispDiseases()
        {
            _dataService.GetDispDiseasesAsync(
                (diseases, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    DispDiseases = new ObservableCollection<DispDisease>(diseases);
                    //Patient.Card.Diseases = diseases;
                }, Patient?.Card);
        }

        //public override void Cleanup()
        //{
        //    // Clean up if needed

        //    base.Cleanup();
        //}
    }
}
