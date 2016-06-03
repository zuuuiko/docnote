using docnote.Model;
using docnote.Resources;
using docnote.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Address _address;
        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                Set(ref _address, value);
            }
        }


        private Card _card;
        public Card Card
        {
            get
            {
                return _card;
            }
            set
            {
                Set(ref _card, value);
            }
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
        public ICommand SavePatientClickCommand { get; private set; }

        [PreferredConstructor]
        public PatientWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            Init();
            Address = new Address();
            CardEntries = new ObservableCollection<CardEntry>();
            Card = new Card { CardEntries = this.CardEntries};
            Patient = new Patient { Addresses = new ObservableCollection<Address> { Address }, Cards = new ObservableCollection<Card> { Card } };
        }

        public PatientWindowVM(Patient p, IDataService dataService)
        {
            _dataService = dataService;
            Patient = p;
            LoadAddress();
            LoadCard();
            LoadCardEntries();
            Init();  
        }

        private void Init()
        {
            PeriodsRadioButtons = PeriodsRadioButtons.All;
            ShowCardEntriesCommand = new RelayCommand<PeriodsRadioButtons>(ShowCardEntries);
            CardEntryDoubleClickCommand = new RelayCommand<CardEntry>(OpenCardEntryWindow);
            AddCardEntryClickCommand = new RelayCommand(OpenCardEntryWindow);
            SavePatientClickCommand = new RelayCommand(SavePatient);
        }

        private void SavePatient()
        {
            //_dataService.AddUpdatePatient(
            //    async (isSaved, error) =>
            //    {
            //        var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            //        if (window != null)
            //            await window.ShowMessageAsync(null, isSaved ? "збережено" : error.Message);
            //    }, Patient);
        }

        private void OpenCardEntryWindow(CardEntry ce)
        {
            CardEntryWindow pw = new CardEntryWindow();
            pw.DataContext = new CardEntryWindowVM(ce, _dataService);
            pw.ShowDialog();
        }
        private void OpenCardEntryWindow()
        {
            CardEntry ce = new CardEntry { CardId = Card.Id, CreationDate = DateTime.Now };
            CardEntryWindow cew = new CardEntryWindow();
            cew.DataContext = new CardEntryWindowVM(ce, _dataService);
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
            _dataService.GetAddressAsync(
                (address, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Address = address;
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
                    Card = card;
                }, Patient);
        }

        private void LoadCardEntries()
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
                }, Card);
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
                }, Card, earliestDate, latestDate);
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
                }, Card, earliestDate);
        }
    }
}
