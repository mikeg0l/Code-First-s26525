using System.ComponentModel.DataAnnotations.Schema;

namespace codefirst.Models;

[Table("Patient")]
public class Patient
{
    [Column("IdPatient")]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}
