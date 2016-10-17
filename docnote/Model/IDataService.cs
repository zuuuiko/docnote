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

        #region Disease
        void GetDiseasesAsync(Action<ObservableCollection<Disease>, Exception> callback, Card c);
        void AddDiseases(Action<bool, Exception> callback, IEnumerable<Disease> diseases, int cardId);
        #endregion

        #region Documents
        void GetDocumentsAsync(Action<ObservableCollection<Document>, Exception> callback, Patient p);
        void AddUpdateDocument(Action<bool, Exception> callback, Document document);
        void DeleteDocument(Action<bool, Exception> callback, Document d);

        #endregion
    }
}
