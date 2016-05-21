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
    class DoctorWindowVM : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Doctor
        private Doctor _doctor;
        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                Set(ref _doctor, value);
            }
        }
        public ICommand DoctorSaveClickCommand { get; private set; }
        #endregion

        public DoctorWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            DoctorSaveClickCommand = new RelayCommand(SaveDoctor);
            LoadDoctor();
        }

        private void SaveDoctor()
        {
            _dataService.UpdateDoctorAsync(
                (isUpdated, error) =>
                {
                    System.Diagnostics.Debug.WriteLine(isUpdated);
                }, Doctor);
        }

        private void LoadDoctor()
        {
            _dataService.GetDoctorAsync(
                (doctor, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Doctor = doctor;
                });
            //this.RaisePropertyChanged(() => this.Patients);
            //Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
        }
    }
}
