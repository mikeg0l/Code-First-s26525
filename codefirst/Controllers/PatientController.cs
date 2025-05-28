using codefirst.Exceptions;
using codefirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace codefirst.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IDbService dbService) : ControllerBase
{
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatientDetails(int patientId)
    {
        try
        {
            var patientDetails = await dbService.GetPatientDetailsAsync(patientId);
            return Ok(patientDetails);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}