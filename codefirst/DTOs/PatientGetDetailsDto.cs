namespace codefirst.DTOs;

public class PatientGetDetailsDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<PrescriptionInPatientContextDto> Prescriptions { get; set; }
}

public class PrescriptionInPatientContextDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
}