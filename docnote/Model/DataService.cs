using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace docnote.Model
{
    public class DataService : IDataService
    {
        #region Patient
        public void AddUpdatePatient(Action<bool, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                if (p.Id != 0) // Update
                {
                    //context.Entry(p.Documents).State = EntityState.Modified;
                    context.Entry(p.Address).State = EntityState.Modified;
                    context.Entry(p.Card).State = EntityState.Modified;
                    context.Entry(p).State = EntityState.Modified;
                }
                else // Save
                {
                    context.Patients.Add(p);
                }

                callback(context.SaveChanges() > 0, null);
            }
        }
        public void DeletePatient(Action<bool, Exception> callback, Patient patient)
        {
            using (var context = new DocnoteContext())
            {
                //http://stackoverflow.com/questions/6746804/code-first-tpt-and-cascade-on-delete
                Patient p = context.Patients
                    .Include(e => e.Documents)
                    .Single(e => e.Id == patient.Id);

                context.Patients.Attach(p);
                context.Patients.Remove(p);
                callback(context.SaveChanges() > 0, null);
            }
        }

        public async void GetPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                await context.Patients.LoadAsync();
                callback(context.Patients.Local, null);
            };
        }

        public async void GetPatientsByLNameAsync(Action<ObservableCollection<Patient>, Exception> callback, string lName)
        {
            using (var context = new DocnoteContext())
            {
                await context.Patients.Where(p => p.LastName.Contains(lName)).LoadAsync();
                callback(context.Patients.Local, null);
            };
        }
        #endregion

        #region Hospital
        public void GetHospital(Action<Hospital, Exception> callback)
        {
            using (var db = new DocnoteContext())
            {
                callback(db.Hospitals.FirstOrDefault(), null);
            };
        }
        public async void GetHospitalAsync(Action<Hospital, Exception> callback)
        {
            using (var db = new DocnoteContext())
            {
                callback(await db.Hospitals.FirstOrDefaultAsync(), null);
            };
        }

        public void UpdateHospital(Action<bool, Exception> callback, Hospital hospital)
        {
            using (var context = new DocnoteContext())
            {
                context.Entry<Hospital>(hospital).State = EntityState.Modified;
                callback(context.SaveChanges() > 0, null);
            }
        }
        #endregion

        #region Doctor
        public async void GetDoctorAsync(Action<Doctor, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                callback(await context.Doctors.FirstOrDefaultAsync(), null);
            };
        }
        public void UpdateDoctor(Action<bool, Exception> callback, Doctor doc)
        {
            using (var context = new DocnoteContext())
            {
                context.Entry<Doctor>(doc).State = EntityState.Modified;
                callback(context.SaveChanges() > 0, null);
            }
        }
        #endregion

        #region Address

        public void GetAddress(Action<Address, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                callback(context.Addresses.Where(a => a.PatientId == p.Id).FirstOrDefault(), null);
            }
        }
        #endregion

        #region Card
        public void GetCard(Action<Card, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                callback(context.Cards.Where(c => c.CardPatientId == p.Id).FirstOrDefault(), null);
            }
        }
        #endregion

        #region CardEntries
        public async void GetCardEntriesAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId).LoadAsync();
                callback(context.CardEntries.Local, null);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId && ce.CreationDate > earliestDate).LoadAsync();
                callback(context.CardEntries.Local, null);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate, DateTime latestDate)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId && ce.CreationDate > earliestDate && ce.CreationDate < latestDate).LoadAsync();
                callback(context.CardEntries.Local, null);
            };
        }

        public void AddUpdateCardEntry(Action<bool, Exception> callback, CardEntry ce)
        {
            using (var context = new DocnoteContext())
            {
                if (ce.Id != 0) // Update
                {
                    context.Entry<CardEntry>(ce).State = EntityState.Modified;
                }
                else // Save
                {
                    //ce.CardId = c.CardPatientId;
                    context.CardEntries.Add(ce);
                }

                callback(context.SaveChanges() > 0, null);
            }
        }

        public void DeleteCardEntry(Action<bool, Exception> callback, CardEntry ce)
        {
            using (var context = new DocnoteContext())
            {
                context.CardEntries.Attach(ce);
                context.CardEntries.Remove(ce);
                callback(context.SaveChanges() > 0, null);
            }
        }
        #endregion

        #region Documents
        public async void GetDocumentsAsync(Action<ObservableCollection<Document>, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                await context.Documents.Where(d => d.PatientId == p.Id).LoadAsync();
                callback(context.Documents.Local, null);
            };
        }

        public void AddUpdateDocument(Action<bool, Exception> callback, Document document)
        {
            using (var context = new DocnoteContext())
            {
                if (document.Id != 0) // Update
                {
                    context.Entry(document).State = EntityState.Modified;
                }
                else // Save
                {
                    context.Documents.Add(document);
                }

                callback(context.SaveChanges() > 0, null);
            }
        }
        #endregion

    }
}