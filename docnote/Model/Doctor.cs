namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Text.RegularExpressions;
    public partial class Doctor : IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string MiddleName { get; set; }

        [StringLength(20)]
        public string Rank { get; set; }

        [StringLength(50)]
        public string JobPlace { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "FirstName":

                        break;
                    case "LastName":

                        break;
                    case "MiddleName":

                        break;
                    case "Rank":

                        break;
                    case "JobPlace":

                        //if (Street.Length > 20)
                        //{
                        //    error = "Допустимо використовувати не більше 20 символів в назві вулиці";
                        //}
                        break;
                    case "PhoneNumber":
                        if (PhoneNumber.Length == 0) break;
                        if (!Regex.IsMatch(PhoneNumber, @"^\(\d{3}\)\d{3}-\d{2}-\d{2}$"))
                        {
                            error = "Введіть телефон у вірному форматі: (ХХХ)ХХХ-ХХ-ХХ";
                        }
                        break;

                }
                return error;
            }
        }
    }
}
