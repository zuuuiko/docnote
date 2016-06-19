namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hospital : IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hospital()
        {
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Region { get; set; }

        [StringLength(20)]
        public string CityVillage { get; set; }

        [StringLength(20)]
        public string Street { get; set; }

        [StringLength(10)]
        public string Building { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctors { get; set; }

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
                    case "Name":
                        
                        break;
                    case "Country":
                        
                        break;
                    case "Region":
                        
                        break;
                    case "CityVillage":
                        
                        break;
                    case "Street":
                        
                        //if (Street.Length > 20)
                        //{
                        //    error = "Допустимо використовувати не більше 20 символів в назві вулиці";
                        //}
                        break;
                    case "Building":
                        
                        break;

                }
                return error;
            }
        }
    }
}
