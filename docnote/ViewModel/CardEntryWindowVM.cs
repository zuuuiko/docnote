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
        Action _updateDiseases;
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

        private ObservableCollection<Disease> _diseases;
        public ObservableCollection<Disease> Diseases
        {
            get { return _diseases; }
            set { Set(ref _diseases, value); }
        }
        private Disease _selectedDisease;
        public Disease SelectedDisease
        {
            get { return _selectedDisease; }
            set { Set(ref _selectedDisease, value); }
        }

        public ICommand SaveAndCloseCardEntryClickCommand { get; private set; }
        public ICommand CloseCardEntryClickCommand { get; private set; }
        public ICommand DiseaseDoubleClickCommand { get; private set; }
        public ICommand DeleteDiseaseClickCommand { get; private set; }

        public CardEntryWindowVM(Action reload, Action reloadDiseases, CardEntry cardEntry, List<Disease> diseases, IDataService dataService)
        {
            _dataService = dataService;
            CardEntry = cardEntry;
            Diseases = new ObservableCollection<Disease>(diseases);
            _updateDiseases = reloadDiseases;
            _updateCardEntriesDataGrid = reload;
            SaveAndCloseCardEntryClickCommand = new RelayCommand(SaveAndCloseCardEntry);
            CloseCardEntryClickCommand = new RelayCommand(CloseCardEntryWindow);
            DiseaseDoubleClickCommand = new RelayCommand<TreeViewItem>(AddDiseaseToDiseases);
            DeleteDiseaseClickCommand = new RelayCommand<Disease>(DeleteDisease);
            //Init(reload);         
        }

        private void DeleteDisease(Disease obj)
        {
            Diseases.Remove(obj);
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
            var disease = new Disease { Code = str.Substring(0, pos), Name = str.Substring(pos + 1)};
            if (!Diseases.Any(d => d.Equals(disease)))
            {
                Diseases.Add(disease);
            }
        }

        private void CloseCardEntryWindow()
        {
            Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault().Close();
        }

        private void SaveAndCloseCardEntry()
        {
            _dataService.AddDiseases((isSaved, error) =>
            {
                if (isSaved)
                {
                    _updateDiseases();
                }
            }, Diseases.ToList(), CardEntry.CardId);

            _dataService.AddUpdateCardEntry(
                async (isSaved, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    var window = Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : error?.Message);//TODO: NO
                        if (result == MessageDialogResult.Affirmative)
                        {
                            _updateCardEntriesDataGrid();
                            CloseCardEntryWindow();
                        }
                    }
                }, CardEntry);
        }
    }
}
