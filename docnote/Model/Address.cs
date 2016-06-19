namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PatientId { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        [StringLength(30)]
        public string Region { get; set; }

        [StringLength(30)]
        public string CityVillage { get; set; }

        public bool? IsSity { get; set; }

        [StringLength(30)]
        public string Street { get; set; }

        [StringLength(10)]
        public string Building { get; set; }

        [StringLength(10)]
        public string Apartment { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
