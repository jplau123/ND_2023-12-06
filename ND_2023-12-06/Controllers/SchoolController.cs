using Microsoft.AspNetCore.Mvc;
using ND_2023_12_06.DTOs;
using ND_2023_12_06.Entities;
using ND_2023_12_06.Interfaces;

namespace ND_2023_12_06.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SchoolController : ControllerBase
{
    private ISchoolService _schoolService;
    private readonly ILogger<SchoolController> _logger;

    public SchoolController(ISchoolService schoolService, ILogger<SchoolController> logger)
    {
        _schoolService = schoolService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartamentas([FromBody] DepartamentasRequest request)
    {
        try
        {
            var departamentas = Departamentas.ConvertFromRequest(request);
            var response = await _schoolService.CreateDepartamentas(departamentas);

            if (!response)
            {
                return BadRequest();
            }

            // TODO implement CreatedAtAction()
            //return CreatedAtAction();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaskaita([FromBody] PaskaitaRequest request)
    {
        try
        {
            var paskaita = Paskaita.ConvertFromRequest(request);

            var response = await _schoolService.CreatePaskaita(paskaita);
            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentas([FromBody] StudentasRequest request)
    {
        try
        {
            var studentas = Studentas.ConvertFromRequest(request);

            var response = await _schoolService.CreateStudentas(studentas);

            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowDepartamentai()
    {
        try
        {
            var result = await _schoolService.ShowDepartamentai();

            // Convert to response object
            var response = result.Select(a => Departamentas.ConvertToResponse(a));

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitos()
    {
        try
        {
            var result = await _schoolService.ShowPaskaitos();
            var response = result.Select(a => Paskaita.ConvertToResponse(a));
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentai()
    {
        try
        {
            var result = await _schoolService.ShowStudentai();
            var response = result.Select(a => Studentas.ConvertToResponse(a));

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDepartamentasForStudentas(Guid DepartamentasId, Guid StudentasId)
    {
        try
        {
            bool response = await _schoolService.UpdateDepartamentasForStudentas(StudentasId, DepartamentasId);

            if (!response)
            {
                return NotFound();
            }
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> AddPaskaitaToDepartamentas(Guid DepartamentasId, Guid PaskaitaId)
    {
        try
        {
            bool response = await _schoolService.UpdatePaskaitaForDepartamentas(PaskaitaId, DepartamentasId);

            if (!response)
            {
                return NotFound();
            }
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> NewPaskaitaToDepartamentas(Guid DepartamentasId, PaskaitaRequest request)
    {
        Paskaita paskaita = Paskaita.ConvertFromRequest(request);

        try
        {
            bool response = await _schoolService.NewPaskaitaToDepartamentas(paskaita, DepartamentasId);

            if (!response)
            {
                return Problem();
            }

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> NewStudentasToDepartamentas(Guid DepartamentasId, StudentasRequest request)
    {
        Studentas studentas = Studentas.ConvertFromRequest(request);

        try
        {
            bool response = await _schoolService.NewStudentasToDepartamentas(studentas, DepartamentasId);

            if (!response)
            {
                return Problem();
            }
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosForStudentas(Guid StudentasId)
    {
        try
        {
            var result = await _schoolService.GetPaskaitosByStudentasId(StudentasId);
            var response = result.Select(a => Paskaita.ConvertToResponse(a));

            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }

    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosInDepartamentas(Guid DepartamentasId)
    {
        try
        {
            var result = await _schoolService.GetPaskaitosInDepartamentas(DepartamentasId);
            var response = result.Select(a => Paskaita.ConvertToResponse(a));

            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentaiInDepartamentas(Guid DepartamentasId)
    {
        try
        {
            var result = await _schoolService.GetStudentaiInDepartamentas(DepartamentasId);
            var response = result.Select(a => Studentas.ConvertToResponse(a));
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
            return BadRequest();
        }
    }
}
