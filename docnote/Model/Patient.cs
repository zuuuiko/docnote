namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Addresses = new HashSet<Address>();
            Cards = new HashSet<Card>();
        }

        public int Id { get; set; }

        public int? DoctorId { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string MiddleName { get; set; }

        [StringLength(30)]
        public string ExLastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public bool? Sex { get; set; }

        public int? IdentificationCode { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string ParentName { get; set; }

        [StringLength(50)]
        public string ParentPhoneNumber { get; set; }

        [StringLength(50)]
        public string JobSchoolPlace { get; set; }

        [StringLength(20)]
        public string JobSchoolPnoneNumber { get; set; }

        [StringLength(20)]
        public string Profession { get; set; }

        [StringLength(20)]
        public string Position { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UnregistrationDate { get; set; }

        [StringLength(20)]
        public string UnregistrationReason { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Cards { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
