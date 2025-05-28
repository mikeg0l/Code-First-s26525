using codefirst.DTOs;
using codefirst.Exceptions;
using codefirst.Models;
using codefirst.Services;
using codefirst.Utils;
using Microsoft.AspNetCore.Mvc;

namespace codefirst.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService dbService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateDto prescriptionCreateDto)
    {
        try
        {
            var prescription = await dbService.CreatePrescriptionAsync(prescriptionCreateDto);
            return Ok(prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}