using codefirst.Data;
using codefirst.DTOs;
using codefirst.Exceptions;
using codefirst.Models;
using codefirst.Utils;
using Microsoft.EntityFrameworkCore;

namespace codefirst.Services;

public interface IDbService
{
    public Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionCreateDt);
    public Task<PatientGetDetailsDto> GetPatientDetailsAsync(int patientId);
}

public class DbService(AppDbContext db) : IDbService
{
    public async Task<PatientGetDetailsDto> GetPatientDetailsAsync(int patientId)
    {
        var patient = await db.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.Id == patientId);

        if (patient == null)
            throw new NotFoundException($"Patient with ID {patientId} does not exist");

        return Mapper.MapPatientToDetailsDto(patient);
    }
    
    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto dto)
    {
        ValidatePrescriptionData(dto);

        var doctor = await GetDoctorAsync(dto.IdDoctor);

        if (dto.Patient == null)
            throw new ArgumentException("Patient information is required");

        var patient = await GetPatientAsync(dto.Patient.Id);
        if (patient == null)
            patient = CreatePatient(dto.Patient);

        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            Doctor = doctor,
            Patient = patient,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var medicamentDto in dto.Medicaments)
        {
            var medicament = await GetMedicamentAsync(medicamentDto.Id);
            var prescriptionMedicament = CreatePrescriptionMedicament(medicament, medicamentDto, prescription);
            prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        db.Prescriptions.Add(prescription);
        await db.SaveChangesAsync();

        return Mapper.MapPrescriptionToDto(prescription);
    }

    private void ValidatePrescriptionData(PrescriptionCreateDto dto)
    {
        if (dto.DueDate < dto.Date)
            throw new ArgumentException("Due date cannot be earlier than prescription date");
        
        if (dto.Medicaments == null || !dto.Medicaments.Any())
            throw new ArgumentException("Prescription must contain at least one medicament");
            
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("Prescription cannot have more than 10 medicaments");
    }

    public async Task<Doctor> GetDoctorAsync(int doctorId)
    {
        var doctor = await db.Doctors.FindAsync(doctorId);
        if (doctor == null)
            throw new NotFoundException($"Doctor with ID {doctorId} does not exist");
        
        return doctor;
    }
    
    public async Task<Patient?> GetPatientAsync(int patientId)
    {
        if (patientId <= 0)
            return null;
        
        return await db.Patients.FindAsync(patientId);
    }

    public Patient CreatePatient(PrescriptionCreateDtoPatient patientDto)
    {
        var patient = new Patient
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            BirthDate = patientDto.Birthdate
        };
    
        db.Patients.Add(patient);
        return patient;
    }
    
    public async Task<Medicament> GetMedicamentAsync(int medicamentId)
    {
        var medicament = await db.Medicaments.FindAsync(medicamentId);
        if (medicament == null)
            throw new NotFoundException($"Medicament with ID {medicamentId} does not exist");

        return medicament;
    }
    
    public PrescriptionMedicament CreatePrescriptionMedicament(
        Medicament medicament, 
        PrescriptionCreateDtoMedicament medicamentDto, 
        Prescription prescription)
    {
        return new PrescriptionMedicament
        {
            Medicament = medicament,
            Prescription = prescription,
            Dose = medicamentDto.Dose,
            Details = medicamentDto.Details
        };
    }
}
