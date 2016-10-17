namespace docnote.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DocnoteContext : DbContext
    {
        static DocnoteContext()
        {
#if DEBUG
            Database.SetInitializer<DocnoteContext>(new ModelInitializer());
#else       
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocnoteContext, docnote.Migrations.Configuration>("ConnectionStringDocnoteDB"));
#endif
        }

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
        public virtual DbSet<Disease> Diseases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();

            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Doctors)
                .WithOptional(e => e.Hospital)
                .HasForeignKey(e => e.HospitalId);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId);

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
                .HasMany(e => e.Diseases)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.CardId)
                .WillCascadeOnDelete();

        }
    }
}
