namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            CardEntries = new HashSet<CardEntry>();
        }

        public int Id { get; set; }

        public int? PatientId { get; set; }

        [StringLength(20)]
        public string CardNameCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CardEntry> CardEntries { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
