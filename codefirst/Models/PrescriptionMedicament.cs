using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace codefirst.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [Column("IdMedicament")]
    public int IdMedicament { get; set; }
    
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    
    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; }

    [ForeignKey("IdMedicament")]
    public virtual Medicament Medicament { get; set; }
    
    [ForeignKey("IdPrescription")]
    public virtual Prescription Prescription { get; set; }
}