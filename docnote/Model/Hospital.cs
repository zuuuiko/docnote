namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hospital
    {
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
    }
}
