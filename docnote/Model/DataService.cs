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
                bool correct = false;
                Exception exeption = null;
                try
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
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        public void DeletePatient(Action<bool, Exception> callback, Patient patient)
        {
            using (var context = new DocnoteContext())
            {
                bool correct = false;
                Exception exeption = null;
                try
                {
                    //http://stackoverflow.com/questions/6746804/code-first-tpt-and-cascade-on-delete
                    Patient p = context.Patients
                        .Include(e => e.Documents)
                        .Single(e => e.Id == patient.Id);

                    context.Patients.Attach(p);
                    context.Patients.Remove(p);
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);

            }
        }

        public async void GetPatientsAsync(Action<ObservableCollection<Patient>, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                ObservableCollection<Patient> patients = null;
                Exception exeption = null;
                try
                {
                    await context.Patients.LoadAsync();
                    patients = context.Patients.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(patients, exeption);
            };
        }

        public async void GetPatientsByLNameAsync(Action<ObservableCollection<Patient>, Exception> callback, string lName)
        {
            using (var context = new DocnoteContext())
            {
                ObservableCollection<Patient> patients = null;
                Exception exeption = null;
                try
                {
                    await context.Patients.Where(p => p.LastName.Contains(lName)).LoadAsync();
                    patients = context.Patients.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(patients, exeption);
            };
        }
        #endregion

        #region Hospital
        public void GetHospital(Action<Hospital, Exception> callback)
        {
            using (var db = new DocnoteContext())
            {
                Exception exeption = null;
                Hospital hospital = null;
                try
                {
                    hospital = db.Hospitals.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(hospital, exeption);
            };
        }
        public async void GetHospitalAsync(Action<Hospital, Exception> callback)
        {
            using (var db = new DocnoteContext())
            {
                Exception exeption = null;
                Hospital hospital = null;
                try
                {
                    hospital = await db.Hospitals.FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(hospital, exeption);
            };
        }

        public void UpdateHospital(Action<bool, Exception> callback, Hospital hospital)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    context.Entry<Hospital>(hospital).State = EntityState.Modified;
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

        #region Doctor
        public async void GetDoctorAsync(Action<Doctor, Exception> callback)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                Doctor doctor = null;
                try
                {
                    doctor = await context.Doctors.FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(doctor, exeption);
            };
        }
        public void UpdateDoctor(Action<bool, Exception> callback, Doctor doc)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    context.Entry<Doctor>(doc).State = EntityState.Modified;
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

        #region Address

        public void GetAddress(Action<Address, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                Address address = null;
                try
                {
                    address = context.Addresses.Where(a => a.PatientId == p.Id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(address, exeption);
            }
        }
        #endregion

        #region Card
        public void GetCard(Action<Card, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                Card card = null;
                try
                {
                    card = context.Cards.Where(c => c.CardPatientId == p.Id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(card, exeption);
            }
        }
        #endregion

        #region CardEntries
        public async void GetCardEntriesAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<CardEntry> cardEntries = null;
                try
                {
                    await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId).LoadAsync();
                    cardEntries = context.CardEntries.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(cardEntries, exeption);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<CardEntry> cardEntries = null;
                try
                {
                    await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId && ce.CreationDate > earliestDate).LoadAsync();
                    cardEntries = context.CardEntries.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(cardEntries, exeption);
            };
        }

        public async void GetCardEntriesByPeriodAsync(Action<ObservableCollection<CardEntry>, Exception> callback, Card c, DateTime earliestDate, DateTime latestDate)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<CardEntry> cardEntries = null;
                try
                {
                    await context.CardEntries.Where(ce => ce.CardId == c.CardPatientId && ce.CreationDate > earliestDate && ce.CreationDate < latestDate).LoadAsync();
                    cardEntries = context.CardEntries.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(cardEntries, exeption);
            };
        }

        public void AddUpdateCardEntry(Action<bool, Exception> callback, CardEntry ce)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
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
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                context.Entry<CardEntry>(ce).State = EntityState.Detached;
                callback(correct, exeption);
            }
        }

        public void DeleteCardEntry(Action<bool, Exception> callback, CardEntry ce)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    context.CardEntries.Attach(ce);
                    context.CardEntries.Remove(ce);
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

        #region InvalidDisease
        public async void GetInvalidDiseasesAsync(Action<ObservableCollection<InvalidDisease>, Exception> callback, Card c)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<InvalidDisease> diseases = null;
                try
                {
                    await context.InvalidDiseases.Where(dis => dis.CardId == c.CardPatientId).LoadAsync();
                    diseases = context.InvalidDiseases.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(diseases, exeption);
            };
        }
        public void AddInvalidDiseases(Action<bool, Exception> callback, IEnumerable<InvalidDisease> diseases, int cardId)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    var contextDiseases = context.InvalidDiseases.Where(dis => dis.CardId == cardId).ToList();
                    foreach (var dis in diseases.Except(contextDiseases))
                    {
                        //if (dis.CardId == 0)
                        //{
                            dis.CardId = cardId;
                            context.InvalidDiseases.Add(dis);
                        //}
                    }
                    foreach (var dis in contextDiseases.Except(diseases))
                    {
                        context.InvalidDiseases.Attach(dis);
                        context.InvalidDiseases.Remove(dis);
                    }
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

        #region DispDisease
        public async void GetDispDiseasesAsync(Action<ObservableCollection<DispDisease>, Exception> callback, Card c)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<DispDisease> diseases = null;
                try
                {
                    await context.DispDiseases.Where(dis => dis.CardId == c.CardPatientId).LoadAsync();
                    diseases = context.DispDiseases.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(diseases, exeption);
            };
        }
        public void AddDispDiseases(Action<bool, Exception> callback, IEnumerable<DispDisease> diseases, int cardId)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    var contextDiseases = context.DispDiseases.Where(dis => dis.CardId == cardId).ToList();
                    foreach (var dis in diseases.Except(contextDiseases))
                    {
                        //if (dis.CardId == 0)
                        //{
                            dis.CardId = cardId;
                            context.DispDiseases.Add(dis);
                        //}
                    }
                    foreach (var dis in contextDiseases.Except(diseases))
                    {
                        context.DispDiseases.Attach(dis);
                        context.DispDiseases.Remove(dis);
                    }
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

        #region CEDisease
        public async void GetCEDiseasesAsync(Action<ObservableCollection<CEDisease>, Exception> callback, CardEntry ce)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<CEDisease> diseases = null;
                try
                {
                    await context.CEDiseases.Where(dis => dis.CardEntryId == ce.Id).LoadAsync();
                    diseases = context.CEDiseases.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(diseases, exeption);
            };
        }
        public void AddCEDiseases(Action<bool, Exception> callback, IEnumerable<CEDisease> diseases, int cardEntryId)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    var contextDiseases = context.CEDiseases.Where(dis => dis.CardEntryId == cardEntryId).ToList();
                    foreach (var dis in diseases.Except(contextDiseases))
                    {
                        //if (dis.CardId == 0)
                        //{
                        dis.CardEntryId = cardEntryId;
                        context.CEDiseases.Add(dis);
                        //}
                    }
                    foreach (var dis in contextDiseases.Except(diseases))
                    {
                        context.CEDiseases.Attach(dis);
                        context.CEDiseases.Remove(dis);
                    }
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }

        #endregion

        #region Documents
        public async void GetDocumentsAsync(Action<ObservableCollection<Document>, Exception> callback, Patient p)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                ObservableCollection<Document> documents = null;
                try
                {
                    await context.Documents.Where(d => d.PatientId == p.Id).LoadAsync();
                    documents = context.Documents.Local;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(documents, exeption);
            };
        }

        public void AddUpdateDocument(Action<bool, Exception> callback, Document document)
        {
            using (var context = new DocnoteContext())
            {
                Exception exeption = null;
                bool correct = false;
                try
                {
                    if (document.Id != 0) // Update
                    {
                        context.Entry<Document>(document).State = EntityState.Modified;
                    }
                    else // Save
                    {
                        context.Documents.Add(document);
                    }
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                context.Entry<Document>(document).State = EntityState.Detached;
                callback(correct, exeption);
            }
        }

        public void DeleteDocument(Action<bool, Exception> callback, Document d)
        {
            using (var context = new DocnoteContext())
            {
                bool correct = false;
                Exception exeption = null;
                try
                {
                    //http://stackoverflow.com/questions/6746804/code-first-tpt-and-cascade-on-delete
                    //Patient p = context.Patients
                    //    .Include(e => e.Documents)
                    //    .Single(e => e.Id == patient.Id);

                    context.Documents.Attach(d);
                    context.Documents.Remove(d);
                    correct = context.SaveChanges() > 0;
                }
                catch (Exception ex)
                {
                    exeption = ex;
                }
                callback(correct, exeption);
            }
        }
        #endregion

    }
}