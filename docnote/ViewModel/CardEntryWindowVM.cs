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

        PatientWindowVM _patientVM;
        private Card _card;

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

        public CardEntryWindowVM(PatientWindowVM patientVM, CardEntry cardEntry, IDataService dataService)
        {
            _dataService = dataService;
            CardEntry = cardEntry;
            Init(patientVM);         
        }
        //new CardEntry
        public CardEntryWindowVM(PatientWindowVM patientVM, Card c, IDataService dataService)
        {//TODO: delete Card, get it from patientVM.Patient.Card
            _dataService = dataService;
            _card = c;
            CardEntry = new CardEntry();
            Init(patientVM);
        }

        private void Init(PatientWindowVM patientVM)
        {
            _patientVM = patientVM;
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
                    var window = Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : error.Message);
                        if (result == MessageDialogResult.Affirmative)
                        {
                            _patientVM.LoadCardEntries();
                            CloseCardEntryWindow();
                        }
                    }
                }, _card, CardEntry);
        }
    }
}
