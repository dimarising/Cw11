using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CW11.Models
{
    public class TabelDbContext : DbContext
    {

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }


        public TabelDbContext(DbContextOptions<TabelDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_PK");
                entity.Property(e => e.IdPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();

            });


            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("IdDoctor_PK");
                entity.Property(e => e.IdDoctor).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).IsRequired();

            });


            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).IsRequired();

            });


            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("IdPrescription_PK");
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(d => d.Patient)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(x => x.IdPatient)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Prescription_Patient");

                entity.HasOne(d => d.Doctor)
                      .WithMany(p => p.Prescriptions)
                      .HasForeignKey(x => x.IdDoctor)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Prescription_Doctor");
            });


            /*            modelBuilder.Entity<Prescription_Medicament>(entity =>
                        {
                            entity.HasKey(e => e.IdMedicament).HasName("IdMedicament_PK");
                            entity.HasKey(e => e.IdPrescription).HasName("IdPrescription_PK");

                            entity.Property(e => e.Details).HasMaxLength(100).IsRequired();

                        });*/



        }

        public void InsertData(ModelBuilder modelBuilder)
        {
            var doctors = new List<Doctor>();
            doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Adam", LastName = "Smith", Email = "Smith@mail.com" });
            doctors.Add(new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Dark", Email = "Anna@gmail.com" });
            doctors.Add(new Doctor { IdDoctor = 3, FirstName = "Ala", LastName = "Kotowa", Email = "Ala@gmail.com" });
            modelBuilder.Entity<Doctor>().HasData(doctors);

            var patients = new List<Patient>();
            patients.Add(new Patient { IdPatient = 1, FirstName = "On", LastName = "Onon", Birthdate = DateTime.Today});
            patients.Add(new Patient { IdPatient = 2, FirstName = "On", LastName = "Ononon", Birthdate = DateTime.Today});
            patients.Add(new Patient { IdPatient = 3, FirstName = "Ona", LastName = "Onaona", Birthdate = DateTime.Today});
            modelBuilder.Entity<Patient>().HasData(patients);

            var medicaments = new List<Medicament>();
            medicaments.Add(new Medicament { IdMedicament = 1, Name = "Magnez", Description = "Opis", Type = "Vitaminy" });
            medicaments.Add(new Medicament { IdMedicament = 2, Name = "Ibuprom", Description = "Od bole", Type = "Lek" });
            medicaments.Add(new Medicament { IdMedicament = 3, Name = "Aspirin", Description = "Przeciwzapalne", Type = "Lek" });
            modelBuilder.Entity<Medicament>().HasData(medicaments);

            var prescriptions = new List<Prescription>();
            prescriptions.Add(new Prescription { IdPrescription = 1, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(5), IdPatient = 1, IdDoctor = 1 });
            prescriptions.Add(new Prescription { IdPrescription = 2, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(5), IdPatient = 2, IdDoctor = 2 });
            prescriptions.Add(new Prescription { IdPrescription = 3, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(5), IdPatient = 3, IdDoctor = 3 });
            modelBuilder.Entity<Prescription>().HasData(prescriptions);

            var prescriptionsMedicaments = new List<Prescription_Medicament>();
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 20, Details = "Details bla" });
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 10, Details = "Details bla" });
            prescriptionsMedicaments.Add(new Prescription_Medicament { IdMedicament = 3, IdPrescription = 3, Dose = 15, Details = "Details bla" });
            modelBuilder.Entity<Prescription_Medicament>().HasData(prescriptionsMedicaments);

        }

    }
}
