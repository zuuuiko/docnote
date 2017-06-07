using docnote.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;

namespace docnote.ViewModel.Documents
{
    class Form_095_o_VM : AbstractFormVM
    {
        private ObservableCollection<CEDisease> _diseasesList;
        public ObservableCollection<CEDisease> DiseasesList
        {
            get { return _diseasesList; }
            set { Set(ref _diseasesList, value); }
        }
        private CEDisease _selectedDisease;
        public CEDisease SelectedDisease
        {
            get { return _selectedDisease; }
            set { Set(ref _selectedDisease, value); }
        }

        private string _diagnosis1;
        public string Diagnosis1
        {
            get { return _diagnosis1; }
            set { Set(ref _diagnosis1, value); (_document as Form_095_o).Diagnosis1 = _diagnosis1; }
        }

        private string _diagnosis2;
        public string Diagnosis2
        {
            get { return _diagnosis2; }
            set { Set(ref _diagnosis2, value); (_document as Form_095_o).Diagnosis2 = _diagnosis2; }
        }

        public ICommand AddDiseases { get; private set; }

        public Form_095_o_VM(Document doc, ICollection<CardEntry> cardEntries, IDataService dataService, Action reloadDocs)
            : base(doc, null, cardEntries, dataService, reloadDocs, @"\f095_oWT.docx")
        {
            Diagnosis1 = (_document as Form_095_o).Diagnosis1;
            Diagnosis2 = (_document as Form_095_o).Diagnosis2;
            InitDiseases(doc.Patient);
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_095_o_Window>().FirstOrDefault();
        }

        protected override void Init(Document doc, ICollection<Patient> patients, ICollection<CardEntry> cardEntries)
        {
            AddDiseases = new RelayCommand<CEDisease>(ChooseDisease);
            var p = doc.Patient;
            _document = new Form_095_o
            {
                HospitalSubordination = Hospital.HospitalSubordination,
                EDRPOU = Hospital.EDRPOU,
                HospitalNamePostAddress = $"{Hospital.Country}, " +
                $"{Hospital.Region}, {Hospital.CityVillage}, " +
                $"{Hospital.Street}, {Hospital.Building}",
                PatientFLMName = $"{p.LastName} {p.FirstName} {p.MiddleName}",
                PatientBirthDate = p.BirthDate,
                SchoolPlace = p.JobSchoolPlace,
                PatientId = p.Id
            };
            //InitDiseases(p);
        }

        private void ChooseDisease(CEDisease obj)
        {
            if (SelectedDisease == null) return;
            if (String.IsNullOrEmpty(Diagnosis1))
            {
                Diagnosis1 = obj.Name;
            }
            else if (String.IsNullOrEmpty(Diagnosis2))
            {
                Diagnosis2 = obj.Name;
            }
            SelectedDisease = null;

        }

        public void InitDiseases(Patient patient)
        {
            _dataService.GetCEDiseasesAsync(
                (diseases, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    DiseasesList = new ObservableCollection<CEDisease>(diseases.Distinct());
                }, patient);
        }
    }
}
