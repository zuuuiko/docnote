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
    class Form_025_6_o_VM : AbstractFormVM
    {
        //PatientWindowVM _patientVM;
        public Form_025_6_o_VM(Document doc, IDataService dataService, Action reloadDocs)
            :base(doc, dataService, reloadDocs, @"\Form_025_6_o_VM.docx")
        {
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_025_6_o_Window>().FirstOrDefault();
        }

        protected override void Init(Document doc)
        {
            //_patientVM = patientVM;
            //if (doc.Id != 0)
            //{
            //    _doc = doc;
            //    return;
            //}
            var p = doc.Patient;
            _doc = new Form_025_6_o
            {
                //DecreeMOZDateNumber
                //HospitalSubordination = doc.Patient.Doctor.Hospital.//
                HospitalNamePostAddress = $"{Hospital.Country}, " +
                $"{Hospital.Region}, {Hospital.CityVillage}, " +
                $"{Hospital.Street}, {Hospital.Building}",
                PatientCardNameCode = p.Card.CardNameCode,
                PatientFLMName = $"{p.LastName} {p.FirstName} {p.MiddleName}",
                PatientSex = p.Sex,
                PatientBirthDate = p.BirthDate,
                PatientStreet = p.Address.Street,
                PatientBuilding = p.Address.Building,
                PatientApartment = p.Address.Apartment,
                //PatientIsWorking,
                //VisitDatesHospital
                //CountVisitsHospital
                //VisitDatesHome
                //CountVisitsHome
            };
        }
    }
}
