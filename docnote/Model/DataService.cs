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
        public void AddUpdatePatient(Action<Patient, Exception> callback, Patient p)
        {
            //using (var context = new DocnoteContext())
            //{
            //    if (p.Id != 0) // Update
            //    {
            //        context.Entry<Patient>(p).State = EntityState.Modified;
            //    }
            //    else // Save
            //    {
            //        context.Patients.Add(p);
            //    }

            //    if(context.SaveChanges() > 0)
            //        callback(p, null);
            //}
        }
        public void DeletePatient(Action<bool, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                context.Patients.Attach(p);
                context.Patients.Remove(p);
                callback(context.SaveChanges() > 0, null);
            }
        }

        public async void GetPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                //db.Doctors.Load();
                await context.Patients.LoadAsync();
                callback(context.Patients.Local, null);
            };
        }
        #endregion

        #region Hospital
        public async void GetHospitalAsync(Action<Hospital, Exception> callback)
        {
            using (var db = new DocnoteContext())
            {
                callback(await db.Hospitals.FirstOrDefaultAsync(), null);
            };
        }

        public async void UpdateHospitalAsync(Action<bool, Exception> callback, Hospital hospital)
        {
            using (var context = new DocnoteContext())
            {
                //Doctor doctorToUpdate = context.Doctors.Where(d => d.Id == doc.Id).FirstOrDefault<Doctor>();
                //doctorToUpdate.LastName = doc.LastName;
                context.Entry<Hospital>(hospital).State = EntityState.Modified;
                callback(await context.SaveChangesAsync() > 0, null);
            }
        }
        #endregion

        #region Doctor
        public async void GetDoctorAsync(Action<Doctor, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                //db.Doctors.Load();
                callback(await context.Doctors.FirstOrDefaultAsync(), null);
            };
        }
        public async void UpdateDoctorAsync(Action<bool, Exception> callback, Doctor doc)
        {
            using (var context = new DocnoteContext())
            {
                //Doctor doctorToUpdate = context.Doctors.Where(d => d.Id == doc.Id).FirstOrDefault<Doctor>();
                //doctorToUpdate.LastName = doc.LastName;
                context.Entry<Doctor>(doc).State = EntityState.Modified;
                callback(await context.SaveChangesAsync() > 0, null);
            }
        }
        #endregion

        public async void GetAddressAsync(Action<Address, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                callback(await context.Addresses.Where(a => a.PatientId == p.Id).FirstOrDefaultAsync(), null);
            }
        }

        public void GetCard(Action<Card, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                callback(context.Cards.Where(c => c.PatientId == p.Id).FirstOrDefault(), null);
            }
        }

        #region CardEntries
        public async void GetCardEntriesAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.Id).LoadAsync();
                callback(context.CardEntries.Local, null);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.Id && ce.CreationDate > earliestDate).LoadAsync();
                callback(context.CardEntries.Local, null);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate, DateTime latestDate)
        {
            using (var context = new DocnoteContext())
            {
                await context.CardEntries.Where(ce => ce.CardId == c.Id && ce.CreationDate > earliestDate && ce.CreationDate < latestDate).LoadAsync();
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
                    context.CardEntries.Add(ce);
                }

                callback(context.SaveChanges() > 0, null);
            }
        }
        #endregion

    }
}