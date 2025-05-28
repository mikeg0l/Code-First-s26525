using codefirst.Models;
using Microsoft.EntityFrameworkCore;

namespace codefirst.Data;

public class AppDbContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    
    public DbSet<Prescription> Prescriptions { get; set; }
    
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public DbSet<Doctor> Doctors { get; set; }
    
    public DbSet<Patient> Patients { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var medicament = new Medicament()
        {
            Id = 1,
            Name = "Ventolin",
            Description = "Medicament for asthma",
            Type = "Non-steroid"
        };

        var doctor = new Doctor()
        {
            Id = 1,
            FirstName = "Albert",
            LastName = "Dough",
            Email = "albert.dough@doctor.com"
        };

        var patient = new Patient()
        {
            Id = 1,
            FirstName = "Boris",
            LastName = "Kabul",
            BirthDate = DateTime.Parse("02-03-2002 04:21:21")
        };

        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
    }
}