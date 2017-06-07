using docnote.Model;
using docnote.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class CardEntryWindowVM : ViewModelBase
    {
        private readonly IDataService _dataService;
        Action _updateCardEntriesDataGrid;
        //PatientWindowVM _patientVM;

        private CardEntry _cardEntry;
        public CardEntry CardEntry
        {
            get
            {
                return _cardEntry;
            }
            set
            {
                Set(ref _cardEntry, value);
            }
        }

        private ObservableCollection<CEDisease> _CEDiseases;
        public ObservableCollection<CEDisease> CEDiseases
        {
            get { return _CEDiseases; }
            set { Set(ref _CEDiseases, value); }
        }
        private CEDisease _selectedCEDisease;
        public CEDisease SelectedCEDisease
        {
            get { return _selectedCEDisease; }
            set { Set(ref _selectedCEDisease, value); }
        }

        public ICommand SaveAndCloseCardEntryClickCommand { get; private set; }
        public ICommand CloseCardEntryClickCommand { get; private set; }
        public ICommand CEDiseaseDoubleClickCommand { get; private set; }
        public ICommand DeleteCEDiseaseClickCommand { get; private set; }

        public CardEntryWindowVM(Action reload, CardEntry cardEntry, IDataService dataService)
        {
            _dataService = dataService;
            CardEntry = cardEntry;
            InitCEDiseases();
            //CEDiseases = new ObservableCollection<CEDisease>();
            _updateCardEntriesDataGrid = reload;
            SaveAndCloseCardEntryClickCommand = new RelayCommand(SaveAndCloseCardEntry);
            CloseCardEntryClickCommand = new RelayCommand(CloseCardEntryWindow);
            CEDiseaseDoubleClickCommand = new RelayCommand<TreeViewItem>(AddDiseaseToDiseases);
            DeleteCEDiseaseClickCommand = new RelayCommand<CEDisease>(DeleteCEDisease);
            //Init(reload);         
        }

        private void DeleteCEDisease(CEDisease obj)
        {
            CEDiseases.Remove(obj);
        }

        //new CardEntry
        //public CardEntryWindowVM(Action reload, IDataService dataService)
        //{
        //    _dataService = dataService;
        //    CardEntry = new CardEntry();
        //    Init(reload);
        //}

        //private void Init(Action reload)
        //{
        //    _updateCardEntriesDataGrid = reload;
        //    SaveAndCloseCardEntryClickCommand = new RelayCommand(SaveAndCloseCardEntry);
        //    CloseCardEntryClickCommand = new RelayCommand(CloseCardEntryWindow);
        //    DiseaseDoubleClickCommand = new RelayCommand<TreeViewItem>(AddDiseaseToDiseases);
        //}

        private void AddDiseaseToDiseases(TreeViewItem obj)
        {
            var str = obj.Header.ToString();
            var pos = str.IndexOf(' ');
            var disease = new CEDisease { Code = str.Substring(0, pos), Name = str.Substring(pos + 1) };
            if (!CEDiseases.Any(d => d.Equals(disease)))
            {
                CEDiseases.Add(disease);
            }
        }

        private void CloseCardEntryWindow()
        {
            Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault().Close();
        }

        private async void SaveAndCloseCardEntry()
        {
            int cardID = CardEntry.CardId;
            _dataService.AddUpdateCardEntry(
                (ce, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    CardEntry = ce;

                }, CardEntry);

            //_dataService.GetCardEntry(
            //    (cardEntry, error) =>
            //    {
            //        if (error != null)
            //        {
            //            MessageBox.Show(error.StackTrace);
            //            return;
            //        }
            //        CardEntry = cardEntry;
            //    }, cardID);

            _dataService.AddCEDiseases((isSaved, error) =>
            {
                if (error != null)
                {
                    MessageBox.Show(error.StackTrace);
                    return;
                }
            }, CEDiseases.ToList(), CardEntry);

            //Добавить Болезнь без кардЭнтри, потом обновить кардЭнтри (добавить ей Болезнь)

            var window = Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault();
            if (window != null)
            {
                var result = await window.ShowMessageAsync(null, "збережено");//TODO: NO
                if (result == MessageDialogResult.Affirmative)
                {
                    _updateCardEntriesDataGrid();
                    CloseCardEntryWindow();
                }
            }
        }

        public void InitCEDiseases()
        {
            _dataService.GetCEDiseasesAsync(
                (diseases, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    CEDiseases = new ObservableCollection<CEDisease>(diseases);
                    //Patient.Card.Diseases = diseases;
                }, CardEntry);
        }
    }
}
