namespace codefirst.DTOs;

public class PrescriptionGetDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public PatientGetDto Patient { get; set; }
    public ICollection<MedicamentGetDto> Medicaments { get; set; }
}
