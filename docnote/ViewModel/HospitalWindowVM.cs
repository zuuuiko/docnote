using docnote.Model;
using docnote.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections;

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
                //ValidateUsername(_username);
            }
        }
        public ICommand HospitalSaveAndCloseClickCommand { get; private set; }
        public ICommand HospitalCloseClickCommand { get; private set; }

        public HospitalWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            HospitalSaveAndCloseClickCommand = new RelayCommand(SaveHospital);
            HospitalCloseClickCommand = new RelayCommand(CloseHospital);
            LoadHospital();
        }

        private void CloseHospital()
        {
            Application.Current.Windows.OfType<HospitalWindow>().FirstOrDefault().Close();
        }

        private void SaveHospital()
        {
            _dataService.UpdateHospital(
                async (isUpdated, error) =>
                {
                    var window = Application.Current.Windows.OfType<HospitalWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isUpdated ? "збережено" : error.Message);
                        if (result == MessageDialogResult.Affirmative) window.Close();
                    }
                }, Hospital);
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
