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
    class Form_Prikr_VM : AbstractFormVM
    {
        public ObservableCollection<PrikrPatientData> PatientItems { get; set; }

        public Form_Prikr_VM(Document doc, ICollection<Patient> patients, ICollection<CardEntry> cardEntries,
                                                                    IDataService dataService, Action reloadDocs)
            : base(doc, patients, null, dataService, reloadDocs, @"\fPrikrWT.docx")
        {
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_Prikr_Window>().FirstOrDefault();
        }

        protected override void Init(Document document, ICollection<Patient> patients, ICollection<CardEntry> cardEntries)
        {
            PatientItems = new ObservableCollection<PrikrPatientData>();
            IEnumerable<PrikrPatientData> prikrPatientDatas = null;
            // init new document
            if (document?.DoctorId == null)
            {
                int rowId = 0;
                prikrPatientDatas = patients.Select(p => new PrikrPatientData
                {
                    RowId = ++rowId,
                    FLMName = $"{p.FirstName} {p.MiddleName} {p.LastName}",
                    BirthDate = p.BirthDate,
                    IdentificationDocumentDetails = p.IdentificationDocumentDetails,
                    City = p.Address?.CityVillage,
                    Street = p.Address?.Street,
                    Building = p.Address?.Building,
                    Apartment = p.Address?.Apartment,
                    UnboundDate = p.UnregistrationDate,
                    UnboundReasonCode = p.UnregistrationReason,
                }).ToArray();
                _document = new Form_Prikr {DoctorId = document?.Doctor?.Id,
                                            PatientsDatas = new HashSet<PrikrPatientData>(prikrPatientDatas) };
            }
            // init exist document
            else
            {
                prikrPatientDatas = LoadPatientsDatas(_document);
                (_document as Form_Prikr).PatientsDatas = new HashSet<PrikrPatientData>(prikrPatientDatas);
            }

            foreach (var item in prikrPatientDatas)
            {
                PatientItems.Add(item);
            }
        }

        private IEnumerable<PrikrPatientData> LoadPatientsDatas(Document document)
        {
            IEnumerable<PrikrPatientData> patientsDatas = null;

            _dataService.GetPrikrPatientDatas(
                (pPDs, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    patientsDatas = pPDs;
                }, document);

            return patientsDatas;
        }
    }
}
