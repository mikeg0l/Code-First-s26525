using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace codefirst.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [Required]
    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }
    [JsonPropertyName("medicaments")]
    public ICollection<PrescriptionCreateDtoMedicament> Medicaments { get; set; }
    [Required]
    [JsonPropertyName("patient")]
    public PrescriptionCreateDtoPatient Patient { get; set; }
    [Required]
    [JsonPropertyName("idDoctor")]
    public int IdDoctor { get; set; }
}

public class PrescriptionCreateDtoMedicament 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("dose")]
    public int Dose { get; set; }
    [JsonPropertyName("details")]
    public string Details { get; set; }
}

public class PrescriptionCreateDtoPatient 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("birthdate")]
    public DateTime Birthdate { get; set; }
}
