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
        public ICommand DoctorSaveAndCloseClickCommand { get; private set; }
        public ICommand DoctorCloseClickCommand { get; private set; }
        #endregion

        public DoctorWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            DoctorSaveAndCloseClickCommand = new RelayCommand(SaveDoctor);
            DoctorCloseClickCommand = new RelayCommand(CloseDoctor);
            LoadDoctor();
        }

        private void CloseDoctor()
        {
            Application.Current.Windows.OfType<DoctorWindow>().FirstOrDefault().Close();
        }

        private void SaveDoctor()
        {
            _dataService.UpdateDoctor(
                async (isUpdated, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    var window = Application.Current.Windows.OfType<DoctorWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isUpdated ? "збережено" : "");//TODO
                        if(result == MessageDialogResult.Affirmative) window.Close();
                    }
                }, Doctor);
        }

        private void LoadDoctor()
        {
            _dataService.GetDoctorAsync(
                (doctor, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Doctor = doctor;
                });
            //this.RaisePropertyChanged(() => this.Patients);
            //Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
        }
    }
}
