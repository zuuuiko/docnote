using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace docnote.Model
{
    public interface IDataService
    {
        #region Patient
        void GetPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback);
        void AddUpdatePatient(Action<bool, Exception> callback, Patient p);
        void DeletePatient(Action<bool, Exception> callback, Patient p);
        void GetPatientsByLNameAsync(Action<ObservableCollection<Patient>, Exception> callback, string lName);
        void GetInvalidDispPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback, bool isInvalid, bool isDisp);
        #endregion

        #region Hospital
        void GetHospital(Action<Hospital, Exception> callback);
        void GetHospitalAsync(Action<Hospital, Exception> callback);
        void UpdateHospital(Action<bool, Exception> callback, Hospital hospital);
        #endregion

        #region Doctor
        void GetDoctorAsync(Action<Doctor, Exception> callback);
        void UpdateDoctor(Action<bool, Exception> callback, Doctor doc);
        #endregion

        #region Address
        void GetAddress(Action<Address, Exception> callback, Patient p);
        #endregion

        #region Card
        void GetCard(Action<Card, Exception> callback, Patient p);
        #endregion

        #region CardEntry
        void GetCardEntriesAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c);

        void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate);

        void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate, DateTime latestDate);

        void AddUpdateCardEntry(Action<bool, Exception> callback, CardEntry ce);

        void DeleteCardEntry(Action<bool, Exception> callback, CardEntry ce);
        #endregion

        #region InvalidDisease
        void GetInvalidDiseasesAsync(Action<ObservableCollection<InvalidDisease>, Exception> callback, Card c);
        void AddInvalidDiseases(Action<bool, Exception> callback, IEnumerable<InvalidDisease> diseases, int cardId);
        #endregion

        #region DispDisease
        void GetDispDiseasesAsync(Action<ObservableCollection<DispDisease>, Exception> callback, Card c);
        void AddDispDiseases(Action<bool, Exception> callback, IEnumerable<DispDisease> diseases, int cardId);
        #endregion

        #region CEDisease
        void GetCEDiseasesAsync(Action<ObservableCollection<CEDisease>, Exception> callback, CardEntry ce);
        void AddCEDiseases(Action<bool, Exception> callback, IEnumerable<CEDisease> diseases, int cardEntryId);
        #endregion

        #region Documents
        void GetDocumentsAsync(Action<ObservableCollection<Document>, Exception> callback, Patient p);
        void AddUpdateDocument(Action<bool, Exception> callback, Document document);
        void DeleteDocument(Action<bool, Exception> callback, Document d);

        void GetPrikrPatientDatas(Action<IEnumerable<PrikrPatientData>, Exception> callback, Document d);
        #endregion
    }
}
