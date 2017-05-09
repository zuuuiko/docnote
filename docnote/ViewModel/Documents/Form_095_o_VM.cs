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

namespace docnote.ViewModel.Documents
{
    class Form_095_o_VM : AbstractFormVM
    {
        public Form_095_o_VM(Document doc, ICollection<CardEntry> cardEntries, IDataService dataService, Action reloadDocs)
            : base(doc, null, cardEntries, dataService, reloadDocs, @"\f095_oWT.docx")
        {
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_095_o_Window>().FirstOrDefault();
        }

        protected override void Init(Document doc, ICollection<Patient> patients, ICollection<CardEntry> cardEntries)
        {
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
        }
    }
}
