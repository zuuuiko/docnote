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
        //ICollection<CardEntry> cardEntries;
        //PatientWindowVM _patientVM;
        public Form_025_6_o_VM(Document doc, ICollection<CardEntry> cardEntries, IDataService dataService, Action reloadDocs)
            :base(doc, cardEntries, dataService, reloadDocs, @"\f025-6_oWT.docx")
        {
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_025_6_o_Window>().FirstOrDefault();
        }

        protected override void Init(Document doc, ICollection<CardEntry> cardEntries)
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
                HospitalSubordination = Hospital.HospitalSubordination,
                EDRPOU = Hospital.EDRPOU,
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
                PatientIsWorking = p.IsWorking,
                //VisitDatesHospital,
                //CountVisitsHospital
                //VisitDatesHome
                //CountVisitsHome
                PatientId = p.Id
            };
            InitVisitsFieilds(cardEntries);
        }

        private void InitVisitsFieilds(ICollection<CardEntry> cardEntries)
        {
            StringBuilder strHome = new StringBuilder();
            StringBuilder strHospital = new StringBuilder();
            byte countHome = 0;
            byte countHospital = 0;
            foreach (var item in cardEntries)
            {
                if (!item.IsHomeVisit.GetValueOrDefault())
                {
                    strHospital.Append(item.CreationDate.ToShortDateString());
                    strHospital.Append(", ");
                    countHospital++;
                }
                else
                {
                    strHome.Append(item.CreationDate.ToShortDateString());
                    strHome.Append(", ");
                    countHome++;
                }
            }
            if (strHospital.Length > 1) strHospital.Remove(strHospital.Length - 2, 2);
            if (strHome.Length > 1) strHome.Remove(strHome.Length - 2, 2);
            Form_025_6_o f = _doc as Form_025_6_o;
            f.VisitDatesHome = strHome.ToString();
            f.VisitDatesHospital = strHospital.ToString();
            f.CountVisitsHome = countHome;
            f.CountVisitsHospital = countHospital;
        }
    }
}
