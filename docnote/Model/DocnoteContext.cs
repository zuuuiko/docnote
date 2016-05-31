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
            //Database.SetInitializer<DocnoteContext>(new DropCreateDatabaseIfModelChanges<DocnoteContext>());
            Database.SetInitializer<DocnoteContext>(new ModelInitializer());
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.Region)
                .IsFixedLength();

            modelBuilder.Entity<Address>()
                .Property(e => e.CityVillage)
                .IsFixedLength();

            modelBuilder.Entity<Address>()
                .Property(e => e.Street)
                .IsFixedLength();

            modelBuilder.Entity<Address>()
                .Property(e => e.Building)
                .IsFixedLength();

            modelBuilder.Entity<Address>()
                .Property(e => e.Apartment)
                .IsFixedLength();

            modelBuilder.Entity<Card>()
                .HasMany(e => e.CardEntries)
                .WithOptional(e => e.Card)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Addresses)
                .WithOptional(e => e.Patient)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Cards)
                .WithOptional(e => e.Patient)
                .WillCascadeOnDelete();
        }
    }
}
