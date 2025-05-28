using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codefirst.Models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    [Column("IdPrescription")]
    public int Id { get; set; }

    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    [ForeignKey("IdDoctor")]
    public virtual Doctor Doctor { get; set; }

    [ForeignKey("IdPatient")]
    public virtual Patient Patient { get; set; }
}