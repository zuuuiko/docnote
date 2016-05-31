using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    class ModelInitializer : DropCreateDatabaseAlways<DocnoteContext>
    {
        protected override void Seed(DocnoteContext context)
        {
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

            context.Doctors.Add(d);
            context.SaveChanges();
            #endregion

            #region Patient
            d = context.Doctors.FirstOrDefault();

            Patient p1 = new Patient
            {
                DoctorId = d.Id,
                FirstName = "P1FName",
                MiddleName = "P1MName",
                LastName = "P1LName",
                ExLastName = "P1ExLName",
                BirthDate = new DateTime(2012, 8, 12),
                Sex = true,
                IdentificationCode = 111123,
                PhoneNumber = "(111)111-11-11",
                ParentName = "P1 Parent Name",
                ParentPhoneNumber = "(111)111-11-22",
                JobSchoolPlace = "P1JobSchoolPlace",
                JobSchoolPnoneNumber = "(111)111-11-33",
                Profession = "P1Profession",
                Position = "P1Position",
                RegistrationDate = new DateTime(2016, 4, 1)
            };

            Patient p2 = new Patient
            {
                DoctorId = d.Id,
                FirstName = "P2FName",
                MiddleName = "P2MName",
                LastName = "P2LName",
                ExLastName = "P2ExLName",
                BirthDate = new DateTime(2010, 9, 23),
                Sex = true,
                IdentificationCode = 222123,
                PhoneNumber = "(222)111-11-11",
                ParentName = "P2 Parent Name",
                ParentPhoneNumber = "(222)111-11-22",
                JobSchoolPlace = "P2JobSchoolPlace",
                JobSchoolPnoneNumber = "(222)111-11-33",
                Profession = "P2Profession",
                Position = "P2Position",
                RegistrationDate = new DateTime(2016, 4, 3)
            };

            Patient p3 = new Patient
            {
                DoctorId = d.Id,
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
                Profession = "P3Profession",
                Position = "P3Position",
                RegistrationDate = new DateTime(2016, 5, 6)
            };

            context.Patients.AddRange(new Patient[] { p1, p2, p3 });
            context.SaveChanges();
            #endregion

            #region Address
            p1 = context.Patients.Find(1);
            p2 = context.Patients.Find(2);

            context.Addresses.AddRange(new Address[]
            {
                new Address
                {
                    PatientId = p1.Id,
                    Country = "Україна",
                    Region = "Івано-Франківська обл.",
                    CityVillage = "Яремче",
                    IsSity = true,
                    Street = "Славська",
                    Building = "87-Б",
                    Apartment = "22"
                }
            });
            #endregion

            #region Cards
            context.Cards.AddRange(new Card[]
            {
                new Card
                {
                    PatientId = p1.Id,
                    CardNameCode = "КД-00234е34",
                    CreationDate = new DateTime(2016, 4, 1),
                    LastUpdateDate = new DateTime(2016, 4, 1)

                },
                new Card
                {
                    PatientId = p2.Id,
                    CardNameCode = "КД-00234е34",
                    CreationDate = new DateTime(2016, 4, 3),
                    LastUpdateDate = new DateTime(2016, 4, 3)
                }
            });
            context.SaveChanges();
            #endregion

            #region CardEntry
            var c1 = context.Cards.Find(1);

            context.CardEntries.AddRange(new CardEntry[]
            {
                new CardEntry
                {
                    CardId = c1.Id,
                    EntryText =
                    @"1. Ця Інструкція визначає порядок заповнення форми первинної облікової документації № 027/о “Виписка із медичної карти амбулаторного (стаціонарного) хворого” (далі - форма № 027/о).

2. Форма № 027/о заповнюється лікарями  закладів охорони здоров’я, які надають амбулаторно-поліклінічну допомогу, при направленні хворого на консультацію в інші заклади охорони здоров’я, на стаціонарне лікування та лікарями стаціонарів при виписці або у випадку смерті хворого.

3. У пунктах 1-3 вказуються прізвище, ім’я, по батькові хворого, дата народження, місце проживання згідно з паспортними даними.

4. У пункті 4 зазначаються місце роботи та займана посада особи.",
                    CreationDate = new DateTime(2016, 4, 1)
                },
                new CardEntry
                {
                    CardId = c1.Id,
                    EntryText =
                    @"5. У пункті 5 вказуються:

в амбулаторно-поліклінічному закладі - дата (число, місяць, рік) початку захворювання та направлення у стаціонар (на консультацію);

у стаціонарі - дата надходження та виписки (смерті) зі стаціонару.

6. У пункті 6 вказуються повний клінічний діагноз основного захворювання, супутні захворювання та ускладнення, які виникли під час стаціонарного (амбулаторного) лікування.

7. У пункті 7 вказуються короткий анамнез, стан при направленні, діагностичні дослідження, перебіг хвороби, проведене лікування.",
                    CreationDate = new DateTime(2016, 4, 17)
                },
                new CardEntry
                {
                    CardId = c1.Id,
                    EntryText =
                    @"8. У пункті 8 вказуються необхідні лікувальні і трудові рекомендації. Для працюючих осіб необхідно вказати терміни тимчасової непрацездатності.

9. У кінці форми проставляються дата заповнення та підпис лікаря, який заповнив виписку на хворого.",
                    CreationDate = new DateTime(2016, 5, 6)
                }
            });
            #endregion

            #region Hospital
            context.Hospitals.AddRange(new Hospital[]
            {
                new Hospital
                {
                    Name = "Національний військово-медичний клінічний центр",
                    Country = "Україна",
                    CityVillage = "Київ",
                    Street = "вулиця Госпітальна",
                    Building = "16"
                }
            });
            context.SaveChanges();
            #endregion
           
        }
    }
}