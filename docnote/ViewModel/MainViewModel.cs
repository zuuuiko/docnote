﻿using GalaSoft.MvvmLight;
using docnote.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows.Controls;
using docnote.View;
using System.Windows;
using System.Linq;

namespace docnote.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;      

        #region Patients
        private ObservableCollection<Patient> _patients;
        //private Patient _selectedPatient;
        public ObservableCollection<Patient> Patients
        {
            get
            {
                return _patients;
            }
            set
            {
                Set(ref _patients, value);
            }
        }
        //public Patient SelectedPatient
        //{
        //    get
        //    {
        //        return _selectedPatient;
        //    }
        //    set
        //    {
        //        Set(ref _selectedPatient, value);
        //    }
        //}
        //public ICommand PatientClickCommand { get; private set; }
        public ICommand PatientDoubleClickCommand { get; private set; }
        public ICommand DeletePatientClickCommand { get; private set; }
        public ICommand AddPatientClickCommand { get; private set; }
        #endregion

        private bool _isShowInvalidPatientsBtnChecked;
        public bool IsShowInvalidPatientsBtnChecked
        {
            get
            {
                return _isShowInvalidPatientsBtnChecked;
            }
            set
            {
                Set(ref _isShowInvalidPatientsBtnChecked, value);
            }
        }

        private bool _isShowDispPatientsBtnChecked;
        public bool IsShowDispPatientsBtnChecked
        {
            get
            {
                return _isShowDispPatientsBtnChecked;
            }
            set
            {
                Set(ref _isShowDispPatientsBtnChecked, value);
            }
        }

        public ICommand OpenDoctorCommand { get; private set; }
        public ICommand OpenHospitalCommand { get; private set; }
        public ICommand SearchPatientsByLNameCommand { get; private set; }
        public ICommand ShowInvalidDispPatientsCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            IsShowInvalidPatientsBtnChecked = false;
            IsShowDispPatientsBtnChecked = false;
            PatientDoubleClickCommand = new RelayCommand<Patient>(OpenPatientWindow);
            AddPatientClickCommand = new RelayCommand(OpenPatientWindow);
            DeletePatientClickCommand = new RelayCommand<Patient>(DeletePatient);
            OpenDoctorCommand = new RelayCommand(OpenDoctorWindow);
            OpenHospitalCommand = new RelayCommand(OpenHospitalWindow);
            SearchPatientsByLNameCommand = new RelayCommand<string>(SearchPatientsByLName);
            ShowInvalidDispPatientsCommand = new RelayCommand(SearchPatientsByInvalidAndDisp);

            LoadPatients();
        }

        private void SearchPatientsByInvalidAndDisp()
        {
            _dataService.GetInvalidDispPatientsAsync(
                (patients, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Patients = patients;
                }, IsShowInvalidPatientsBtnChecked, IsShowDispPatientsBtnChecked);
        }

        private void SearchPatientsByLName(string lName)
        {
            _dataService.GetPatientsByLNameAsync(
                (patients, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Patients = patients;
                }, lName);
        }

        private async void DeletePatient(Patient p)
        {
            if (p == null) return;

            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window != null)
            {
                 var result = await window.ShowMessageAsync("Видалити?", $"{p.FirstName} {p.LastName}", 
                                                                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;
            }
           
            _dataService.DeletePatient(
                async (isDeleted, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    if (window != null && isDeleted)
                        await window.ShowMessageAsync(null, $"{p.FirstName} {p.LastName} видалений");
                }, p);
            LoadPatients();
        }

        private void OpenHospitalWindow()
        {
            HospitalWindow dw = new HospitalWindow();
            dw.DataContext = new HospitalWindowVM(_dataService);
            dw.ShowDialog();
        }

        private void OpenDoctorWindow()
        {
            var dw = new DoctorWindow();
            dw.DataContext = new DoctorWindowVM(_dataService, Patients);
            dw.ShowDialog();
        }

        private void OpenPatientWindow(Patient p)
        {
            PatientWindow pw = new PatientWindow();
            pw.DataContext = new PatientWindowVM(LoadPatients, p, _dataService);
            //pw.Owner = this;
            pw.ShowDialog();
        }
        //New Patient
        private void OpenPatientWindow()
        {
            PatientWindow pw = new PatientWindow();
            pw.DataContext = new PatientWindowVM(LoadPatients, _dataService);
            //pw.Owner = this;
            pw.ShowDialog();
        }

        public void LoadPatients()
        {
            _dataService.GetPatientsAsync(
                (patients, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Patients = patients;
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}