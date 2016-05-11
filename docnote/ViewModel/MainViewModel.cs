using GalaSoft.MvvmLight;
using docnote.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;
using docnote.View;

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

        private DataGridRowDetailsVisibilityMode _rowDetailsVisible;
        public DataGridRowDetailsVisibilityMode RowDetailsVisible
        {
            get
            {
                return _rowDetailsVisible;
            }
            set
            {
                Set(ref _rowDetailsVisible, value);
            }
        }


        #region Hospital
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
        #endregion

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

        #region Patients
        private ObservableCollection<Patient> _patients;
        private Patient _selectedPatient;
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
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                Set(ref _selectedPatient, value);
            }
        }
        public ICommand PatientClickCommand { get; private set; }
        public ICommand PatientDoubleClickCommand { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _rowDetailsVisible = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            DoctorSaveClickCommand = new RelayCommand(SaveDoctor);
            PatientClickCommand = new RelayCommand(() =>
            {
                if (RowDetailsVisible == DataGridRowDetailsVisibilityMode.Collapsed)
                {
                    RowDetailsVisible = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                }
                else
                {
                    RowDetailsVisible = DataGridRowDetailsVisibilityMode.Collapsed;
                }
            });

            PatientDoubleClickCommand = new RelayCommand<Patient>(OpenPatientWindow);

            LoadPatients();
            LoadDoctor();
            LoadHospital();

        }

        private void OpenPatientWindow(Patient p)
        {
            PatientWindow pw = new PatientWindow();
            pw.DataContext = new PatientWindowVM(p, _dataService);
            //pw.Owner = this;
            pw.ShowDialog();
        }

        private void LoadPatients()
        {
            _dataService.GetPatientsAsync(
                (patients, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Patients = patients;
                });
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

        private void SaveDoctor()
        {
            _dataService.UpdateDoctorAsync(
                (isUpdated, error)=>
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


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}