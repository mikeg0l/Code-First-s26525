using codefirst.DTOs;
using codefirst.Models;

namespace codefirst.Utils;

public class Mapper
{
    public static PrescriptionGetDto MapPrescriptionToDto(Prescription prescription)
    {
        return new PrescriptionGetDto
        {
            Id = prescription.Id,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorGetDto
            {
                Id = prescription.Doctor.Id,
                FirstName = prescription.Doctor.FirstName,
                LastName = prescription.Doctor.LastName,
                Email = prescription.Doctor.Email
            },
            Patient = new PatientGetDto
            {
                Id = prescription.Patient.Id,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate
            },
            Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentGetDto
            {
                Id = pm.Medicament.Id,
                Name = pm.Medicament.Name,
                Description = pm.Medicament.Description,
                Type = pm.Medicament.Type,
                Dose = pm.Dose,
                Details = pm.Details
            }).ToList()
        };
    }
    
    public static PatientGetDetailsDto MapPatientToDetailsDto(Patient patient)
    {
        return new PatientGetDetailsDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new PrescriptionInPatientContextDto
                {
                    Id = p.Id,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Doctor = new DoctorGetDto
                    {
                        Id = p.Doctor.Id,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentGetDto
                    {
                        Id = pm.Medicament.Id,
                        Name = pm.Medicament.Name,
                        Description = pm.Medicament.Description,
                        Type = pm.Medicament.Type,
                        Dose = pm.Dose,
                        Details = pm.Details
                    }).ToList()
                }).ToList()
        };
    } 
}