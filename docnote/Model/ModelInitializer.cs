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
            #region CardEntry
            var ce1 = new CardEntry
            {
                EntryText =
                @"1. Ця Інструкція визначає порядок заповнення форми первинної облікової документації № 027/о “Виписка із медичної карти амбулаторного (стаціонарного) хворого” (далі - форма № 027/о).

2. Форма № 027/о заповнюється лікарями  закладів охорони здоров’я, які надають амбулаторно-поліклінічну допомогу, при направленні хворого на консультацію в інші заклади охорони здоров’я, на стаціонарне лікування та лікарями стаціонарів при виписці або у випадку смерті хворого.

3. У пунктах 1-3 вказуються прізвище, ім’я, по батькові хворого, дата народження, місце проживання згідно з паспортними даними.

4. У пункті 4 зазначаються місце роботи та займана посада особи.",
                CreationDate = new DateTime(2016, 4, 1)
            };
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
            var ce3 = new CardEntry
            {
                EntryText =
                    @"8. У пункті 8 вказуються необхідні лікувальні і трудові рекомендації. Для працюючих осіб необхідно вказати терміни тимчасової непрацездатності.

9. У кінці форми проставляються дата заповнення та підпис лікаря, який заповнив виписку на хворого.",
                CreationDate = new DateTime(2016, 5, 6)
            };
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
            #endregion

            #region Cards

            var c1 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 1),
                LastUpdateDate = new DateTime(2016, 4, 1)

            };
            var c2 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 3),
                LastUpdateDate = new DateTime(2016, 4, 3)
            };
            var c3 = new Card
            {
                CardNameCode = "КД-00234е34",
                CreationDate = new DateTime(2016, 4, 3),
                LastUpdateDate = new DateTime(2016, 4, 3)
            };

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
            var doc2 = new Form_063_o
            {
                LastName = "Petrov",
                TestText = "testText2"
            };

            #endregion

            #region Patient
            Patient p1 = new Patient
            {
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
                RegistrationDate = new DateTime(2016, 4, 1),
                Card = c1,
                Address = a1
            };
            p1.Documents.Add(doc1);
            p1.Documents.Add(doc2);

            Patient p2 = new Patient
            {
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
                Profession = "P2Profession",
                Position = "P2Position",
                RegistrationDate = new DateTime(2016, 4, 3),
                Card = c2,
                Address = a2
            };

            Patient p3 = new Patient
            {
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
                RegistrationDate = new DateTime(2016, 5, 6),
                Card = c3,
                Address = a3
            };

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

            #endregion

            #region Hospital
            var h = new Hospital
            {
                Name = "Національний військово-медичний клінічний центр",
                Country = "Україна",
                CityVillage = "Київ",
                Street = "вулиця Госпітальна",
                Building = "16"
            };
            h.Doctors.Add(d);
            context.Hospitals.Add(h);
            context.SaveChanges();
            #endregion
        }
    }
}