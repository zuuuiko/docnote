namespace docnote.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient
    {
        public int Id { get; set; }

        public int? DoctorId { get; set; }

        [StringLength(100)]
        public string IdentificationDocumentDetails { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string MiddleName { get; set; }

        [StringLength(30)]
        public string ExLastName { get; set; }

        [StringLength(30)]
        public string SocialStatus { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool? Sex { get; set; }

        public int? IdentificationCode { get; set; }

        [StringLength(14)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string ParentName { get; set; }

        [StringLength(50)]
        public string ParentPhoneNumber { get; set; }

        [StringLength(50)]
        public string JobSchoolPlace { get; set; }

        [StringLength(20)]
        public string JobSchoolPnoneNumber { get; set; }

        public bool? IsWorking { get; set; }

        [StringLength(20)]
        public string Profession { get; set; }

        [StringLength(20)]
        public string Position { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime? RegistrationDate { get; set; }

        public DateTime? UnregistrationDate { get; set; }

        public bool? UnregistrationReason { get; set; }

        public virtual Address Address { get; set; }

        public virtual Card Card { get; set; }

        public virtual Doctor Doctor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Documents = new HashSet<Document>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
    }
}
