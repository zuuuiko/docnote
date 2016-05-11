using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace docnote.Model
{
    public interface IDataService
    {
        void GetPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback);
        void GetPatient(Action<Patient, Exception> callback, Patient p);
        void GetPatientById(Action<Patient, Exception> callback, int id);
        void AddPatientAsync(Action<Patient, Exception> callback, Patient p);
        void UpdatePatient(Action<Patient, Exception> callback, Patient p);
        void DeletePatient(Action<bool, Exception> callback, Patient p);
        
        #region Hospital
        void GetHospitalAsync(Action<Hospital, Exception> callback);
        #endregion

        #region Doctor
        void GetDoctorAsync(Action<Doctor, Exception> callback);
        void UpdateDoctorAsync(Action<bool, Exception> callback, Doctor doc);
        #endregion

        void GetAddressAsync(Action<Address, Exception> callback, Patient p);

        void GetCard(Action<Card, Exception> callback, Patient p);

        void GetCardEntriesAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c);

        void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate);

        void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate, DateTime latestDate);

        void SaveCardEntryAsync(Action<bool, Exception> callback, CardEntry ce);

    }
}
