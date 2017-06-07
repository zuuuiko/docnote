namespace docnote.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DocnoteContext : DbContext
    {
        public DocnoteContext()
            : base("name=ConnectionStringDocnoteDB")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CardEntry> CardEntries { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Form_Prikr> Form_PrikrDocuments { get; set; }
        public virtual DbSet<PrikrPatientData> PrikrPatientDatas { get; set; }

        public virtual DbSet<InvalidDisease> InvalidDiseases { get; set; }
        public virtual DbSet<CEDisease> CEDiseases { get; set; }
        public virtual DbSet<DispDisease> DispDiseases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Doctors)
                .WithOptional(e => e.Hospital)
                .HasForeignKey(e => e.HospitalId);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Doctor>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .HasOptional(e => e.Address)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.Patient)
                .HasForeignKey(e => e.PatientId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Patient>()
                .HasOptional(e => e.Card)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Card>()
                .HasMany(e => e.CardEntries)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.CardId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Card>()
                .HasMany(e => e.InvalidDiseases)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.CardId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Card>()
                .HasMany(e => e.DispDiseases)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.CardId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CardEntry>()
                .HasMany(e => e.CEDiseases)
                .WithRequired(e => e.CardEntry)
                .HasForeignKey(e => e.CardEntryId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Form_Prikr>()
                .HasMany(e => e.PatientsDatas)
                .WithOptional(e => e.FormPrikr)
                .HasForeignKey(e => e.FormPrikrId)
                .WillCascadeOnDelete();
        }
    }
}
