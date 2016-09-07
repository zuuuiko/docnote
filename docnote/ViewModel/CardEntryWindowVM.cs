using docnote.Model;
using docnote.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public ICommand SaveAndCloseCardEntryClickCommand { get; private set; }
        public ICommand CloseCardEntryClickCommand { get; private set; }

        public CardEntryWindowVM(Action reload, CardEntry cardEntry, IDataService dataService)
        {
            _dataService = dataService;
            CardEntry = cardEntry;
            Init(reload);         
        }
        //new CardEntry
        //public CardEntryWindowVM(Action reload, IDataService dataService)
        //{
        //    _dataService = dataService;
        //    CardEntry = new CardEntry();
        //    Init(reload);
        //}

        private void Init(Action reload)
        {
            _updateCardEntriesDataGrid = reload;
            SaveAndCloseCardEntryClickCommand = new RelayCommand(SaveAndCloseCardEntry);
            CloseCardEntryClickCommand = new RelayCommand(CloseCardEntryWindow);
        }

        private void CloseCardEntryWindow()
        {
            Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault().Close();
        }

        private void SaveAndCloseCardEntry()
        {
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
