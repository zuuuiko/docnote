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

        public ICommand SaveCardEntryClickCommand { get; private set; }

        public CardEntryWindowVM(CardEntry cardEntry, IDataService dataService)
        {
            _dataService = dataService;
            CardEntry = cardEntry;
            SaveCardEntryClickCommand = new RelayCommand(SaveCardEntryWindow);
        }

        private void SaveCardEntryWindow()
        {
            _dataService.AddUpdateCardEntry(
                async (isSaved, error) =>
                {
                    var window = Application.Current.Windows.OfType<CardEntryWindow>().FirstOrDefault();
                    if (window != null)
                        await window.ShowMessageAsync(null, isSaved ? "збережено" : error.Message);
                }, CardEntry);
        }
    }
}
