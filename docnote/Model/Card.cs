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
            InvalidDiseases = new HashSet<InvalidDisease>();
            DispDiseases = new HashSet<DispDisease>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CardPatientId { get; set; }

        [StringLength(20)]
        public string CardNameCode { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public bool? IsInvalid { get; set; } = false;
        public bool? IsDisp { get; set; } = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CardEntry> CardEntries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvalidDisease> InvalidDiseases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DispDisease> DispDiseases { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
