using docnote.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace docnote.ViewModel
{
    public class HospitalWindowVM : ViewModelBase
    {
        private readonly IDataService _dataService;

        private Hospital _hospital;
        public Hospital Hospital
        {
            get
            {
                return _hospital;
            }
            set
            {
                Set(ref _hospital, value);
            }
        }
        public ICommand HospatalSaveClickCommand { get; private set; }

        public HospitalWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            LoadHospital();
        }

        private void LoadHospital()
        {
            _dataService.GetHospitalAsync(
                (hospital, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Hospital = hospital;
                });
        }
    }
}
