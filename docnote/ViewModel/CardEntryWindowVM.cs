using docnote.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _dataService.SaveCardEntryAsync(
                (isSaved, error) =>
                {
                    System.Diagnostics.Debug.WriteLine(isSaved);
                }, CardEntry);

        }
    }
}
