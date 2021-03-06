﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
#if DEBUG
    class ModelInitializer : DropCreateDatabaseAlways<DocnoteContext>
#else
    class ModelInitializer : CreateDatabaseIfNotExists<DocnoteContext>
#endif
    {
        protected override void Seed(DocnoteContext context)
        {
#if DEBUG
            #region CEDisease
            var dis1 = new CEDisease { Code = "C02.0", Name = "Спинки языка" };
            var dis2 = new CEDisease { Code = "C02.1", Name = "Боковой поверхности языка" };
            var dis3 = new CEDisease { Code = "D00-D09", Name = "Новообразования in situ" };
            #endregion

            #region InvalidDisease
            var invdis1 = new InvalidDisease { Code = "A00", Name = "Холера" };
            var invdis2 = new InvalidDisease { Code = "A01", Name = "Тиф и паратиф" };
            var invdis3 = new InvalidDisease { Code = "A15-A19", Name = "Туберкулез" };
            #endregion

            #region DispDisease
            var dispdis1 = new DispDisease { Code = "A00", Name = "Холера" };
            var dispdis2 = new DispDisease { Code = "C02.1", Name = "Спинки языка" };
            var dispdis3 = new DispDisease { Code = "C02", Name = "Злокачественное новообразование других и неуточненных частей языка" };
            #endregion

            #region CardEntry
            #region ce1
            var ce1 = new CardEntry
            {
                EntryText =
                @"1. Ця Інструкція визначає порядок заповнення форми первинної облікової документації № 027/о “Виписка із медичної карти амбулаторного (стаціонарного) хворого” (далі - форма № 027/о).

2. Форма № 027/о заповнюється лікарями  закладів охорони здоров’я, які надають амбулаторно-поліклінічну допомогу, при направленні хворого на консультацію в інші заклади охорони здоров’я, на стаціонарне лікування та лікарями стаціонарів при виписці або у випадку смерті хворого.

3. У пунктах 1-3 вказуються прізвище, ім’я, по батькові хворого, дата народження, місце проживання згідно з паспортними даними.

4. У пункті 4 зазначаються місце роботи та займана посада особи.",
                CreationDate = new DateTime(2016, 4, 1)
            };
            #endregion
            #region ce2
            var ce2 = new CardEntry
            {
                EntryText =
                    @"5. У пункті 5 вказуються:

в амбулаторно-поліклінічному закладі - дата (число, місяць, рік) початку захворювання та направлення у стаціонар (на консультацію);

у стаціонарі - дата надходження та виписки (смерті) зі стаціонару.

6. У пункті 6 вказуються повний клінічний діагноз основного захворювання, супутні захворювання та ускладнення, які виникли під час стаціонарного (амбулаторного) лікування.

7. У пункті 7 вказуються короткий анамнез, стан при направленні, діагностичні дослідження, перебіг хвороби, проведене лікування.",
                CreationDate = new DateTime(2016, 4, 17)
            };
            #endregion
            #region ce3
            var ce3 = new CardEntry
            {
                EntryText =
                    @"8. У пункті 8 вказуються необхідні лікувальні і трудові рекомендації. Для працюючих осіб необхідно вказати терміни тимчасової непрацездатності.

9. У кінці форми проставляються дата заповнення та підпис лікаря, який заповнив виписку на хворого.",
                CreationDate = new DateTime(2016, 5, 6)
            };
            #endregion
            var ce4 = new CardEntry
            {
                EntryText = null,
                CreationDate = new DateTime(2016, 5, 6)
            };
            var ce5 = new CardEntry
            {
                EntryText = null,
                CreationDate = new DateTime(2016, 5, 6)
            };

            ce1.CEDiseases.Add(dis1);
            ce1.CEDiseases.Add(dis2);
            ce1.CEDiseases.Add(dis3);
            #endregion

            #region Cards

            var c1 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 1),
                LastUpdateDate = new DateTime(2016, 4, 1),
                IsInvalid = true
            };
            var c2 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 3),
                LastUpdateDate = new DateTime(2016, 4, 3),
                IsInvalid = true,
                IsDisp = true
            };
            var c3 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 3),
                LastUpdateDate = new DateTime(2016, 4, 3),
                IsDisp = true
            };
            c1.InvalidDiseases.Add(invdis1);
            c1.InvalidDiseases.Add(invdis2);
            c1.InvalidDiseases.Add(invdis3);
            c1.DispDiseases.Add(dispdis1);
            c1.DispDiseases.Add(dispdis2);
            c1.DispDiseases.Add(dispdis3);
            c1.CardEntries.Add(ce1);
            c1.CardEntries.Add(ce2);
            c1.CardEntries.Add(ce3);
            c2.CardEntries.Add(ce4);
            c3.CardEntries.Add(ce5);
            #endregion

            #region Address
            var a1 = new Address
            {
                Country = "Україна",
                Region = "Івано-Франківська обл.",
                CityVillage = "Яремче",
                IsSity = true,
                Street = "Славська",
                Building = "87-Б",
                Apartment = "22"
            };

            var a2 = new Address
            {
                Country = null,
                Region = null,
                CityVillage = null,
                IsSity = null,
                Street = null,
                Building = null,
                Apartment = null
            };
            var a3 = new Address
            {
                Country = null,
                Region = null,
                CityVillage = null,
                IsSity = null,
                Street = null,
                Building = null,
                Apartment = null
            };

            #endregion

            #region Document
            var doc1 = new Form_063_o
            {
                LastName = "Ivanov",
                TestText = "testText1"
            };
            var doc2 = new Form_025_6_o
            {
                HospitalSubordination = "Міністерство охорони та здоров'я України",
                HospitalNamePostAddress = "01234, Україна, м. Київ, вул. Алішера Навої, 1",
                EDRPOU = "0123456789",
                OpeningDoctorName = "Петренко Петро Петрович",
                PatientCardNameCode = "КД-00234е34",
                PatientFLMName = "Іваненко Іван Іванович",
                PatientSex = true,
                PatientBirthDate = new DateTime(2012, 8, 12),
                PatientStreet = "Славська",
                PatientBuilding = "87-Б",
                PatientApartment = "22",
                PatientIsWorking = false,
                Contingent = 3,
                Purpose = 4,
                IsFirstVisit = false,
                VisitDatesHospital = new DateTime(2016, 8, 12).ToShortDateString() + ", " +
                                    new DateTime(2016, 8, 13).ToShortDateString() + ", " +
                                    new DateTime(2016, 9, 14).ToShortDateString(),
                CountVisitsHospital = 3,
                VisitDatesHome = new DateTime(2016, 8, 15).ToShortDateString() + ", " +
                                    new DateTime(2016, 9, 16).ToShortDateString(),
                CountVisitsHome = 2,
                DiagnosisMain = "головний діагноз",
                DiagnosisCode = "код123321",
                DiagnosisSecondary = "супутні діагнози",
                DiagnosisSeverity = 3,
                TraumaPlace = 5,
                OperationName = "назва операції",
                DispGroupIsOnRegister = 3,
                DispGroupTakenOnRegister = 3,
                DispRemovedRegisterDate = new DateTime(2016, 8, 12),
                DispRemovedRegisterReason = 3,
                DispDateOfNextVisit = new DateTime(2016, 8, 12),
                InvGroupFirst = 3,
                InvGroupChornobyl = 3,
                InvGroupConfirmed = 3,
                IllSheetCode = "11somecode1",
                IllSheetOpenDate = new DateTime(2016, 8, 12),
                IllSheetCloseDate = new DateTime(2016, 8, 12),
                DovidkaOpenDate = new DateTime(2016, 8, 12),
                DovidkaCloseDate = new DateTime(2016, 8, 12),
                TreatmentResult = 3,
                ServiceCase = false,
                ClosingDoctorName = "Гарбуз Дмитро",
                SignDate = DateTime.Now.Date
            };

            #region PersonsDatas
            List<PrikrPatientData> personsDatas = new List<PrikrPatientData>();
            for (int i = 1; i <= 30; i++)
            {
                personsDatas.Add(new PrikrPatientData
                {
                    RowId = i,
                    IdentificationDocumentDetails = "EH 000000",
                    FLMName = "Іваненко Іван Іванович",
                    BirthDate = new DateTime(2012, 8, 12),
                    City = "Kiev",
                    Street = "Some street",
                    Building = "5B",
                    Apartment = "3",
                    SignDate = new DateTime(2017, 01, 12),
                    UnboundDate = new DateTime(2017, 02, 13),
                    UnboundReasonCode = true
                });
            }
            #endregion

            var doc3 = new Form_Prikr();
            personsDatas.ForEach(pPD => doc3.PatientsDatas.Add(pPD));

            #endregion

            #region Patient
            #region p1
            Patient p1 = new Patient
            {
                IdentificationDocumentDetails = "EH 000000",
                FirstName = "P1FName",
                MiddleName = "P1MName",
                LastName = "P1LName",
                ExLastName = "P1ExLName",
                SocialStatus = "АТОшник",
                BirthDate = new DateTime(2012, 8, 12),
                Sex = true,
                IdentificationCode = 111123,
                PhoneNumber = "(111)111-11-11",
                ParentName = "P1 Parent Name",
                ParentPhoneNumber = "(111)111-11-22",
                JobSchoolPlace = "P1JobSchoolPlace",
                JobSchoolPnoneNumber = "(111)111-11-33",
                IsWorking = false,
                Profession = "P1Profession",
                Position = "P1Position",
                RegistrationDate = new DateTime(2016, 4, 1),
                Card = c1,
                Address = a1
            };
            #endregion
            p1.Documents.Add(doc1);
            p1.Documents.Add(doc2);

            #region p2
            Patient p2 = new Patient
            {
                IdentificationDocumentDetails = "EH 000001",
                FirstName = "P2FName",
                MiddleName = "P2MName",
                LastName = "P2LName",
                ExLastName = "P2ExLName",
                BirthDate = new DateTime(2010, 9, 23),
                Sex = null,
                IdentificationCode = 222123,
                PhoneNumber = "(222)111-11-11",
                ParentName = "P2 Parent Name",
                ParentPhoneNumber = "(222)111-11-22",
                JobSchoolPlace = "P2JobSchoolPlace",
                JobSchoolPnoneNumber = "(222)111-11-33",
                IsWorking = true,
                Profession = "P2Profession",
                Position = "P2Position",
                RegistrationDate = new DateTime(2016, 4, 3),
                Card = c2,
                Address = a2
            };
            #endregion
            #region p3
            Patient p3 = new Patient
            {
                IdentificationDocumentDetails = "EH 000002",
                FirstName = "P3FName",
                MiddleName = "P3MName",
                LastName = "P3LName",
                ExLastName = "P3ExLName",
                BirthDate = new DateTime(2016, 4, 7),
                Sex = false,
                IdentificationCode = 333123,
                PhoneNumber = "(333)111-11-11",
                ParentName = "P2 Parent Name",
                ParentPhoneNumber = "(333)111-11-22",
                JobSchoolPlace = "P3JobSchoolPlace",
                JobSchoolPnoneNumber = "(333)111-11-33",
                IsWorking = false,
                Profession = "P3Profession",
                Position = "P3Position",
                RegistrationDate = new DateTime(2016, 5, 6),
                Card = c3,
                Address = a3
            };
            #endregion
            #endregion

            #region Doctor
            Doctor d = new Doctor
            {
                FirstName = "DoctorFName",
                MiddleName = "DoctorMName",
                LastName = "DoctorLName",
                Rank = "DoctorRank",
                JobPlace = "DoctorJobPlace",
                PhoneNumber = "(000)000-00-00"
            };

            d.Patients.Add(p1);
            d.Patients.Add(p2);
            d.Patients.Add(p3);
            d.Documents.Add(doc3);
            #endregion

            #region Hospital
            var h = new Hospital
            {
                Name = "Національний військово-медичний клінічний центр",
                Country = "Україна",
                CityVillage = "Київ",
                Street = "вулиця Госпітальна",
                Building = "16",
                HospitalSubordination = "Міністерство охорони здоров'я України",
                EDRPOU = "0123456789"
            };
            h.Doctors.Add(d);
            context.Hospitals.Add(h);
            #endregion
#else
            Doctor d = new Doctor { LastName = "NO Name" };
            var h = new Hospital { Name = "NO Name" };
            h.Doctors.Add(d);
            context.Hospitals.Add(h);
#endif
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.StackTrace);
            }

        }
    }
}