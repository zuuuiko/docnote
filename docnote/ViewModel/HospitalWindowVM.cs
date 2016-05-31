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
        public ICommand HospitalSaveClickCommand { get; private set; }

        public HospitalWindowVM(IDataService dataService)
        {
            _dataService = dataService;
            HospitalSaveClickCommand = new RelayCommand(SaveHospital);
            LoadHospital();
        }

        private void SaveHospital()
        {
            _dataService.UpdateHospitalAsync(
                async (isUpdated, error) =>
                {
                    var window = Application.Current.Windows.OfType<HospitalWindow>().FirstOrDefault();
                    if (window != null)
                        await window.ShowMessageAsync(null, isUpdated ? "збережено" : error.Message);
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
