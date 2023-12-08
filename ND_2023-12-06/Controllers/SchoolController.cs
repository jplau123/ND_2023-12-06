using Microsoft.AspNetCore.Mvc;
using ND_2023_12_06.DTOs;
using ND_2023_12_06.Entities;
using ND_2023_12_06.Helpers;
using ND_2023_12_06.Interfaces;

namespace ND_2023_12_06.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SchoolController : ControllerBase
{
    private readonly ISchoolService _schoolService;
    private readonly ILogger<SchoolController> _logger;
    private readonly ResponseHelper _handler;

    public SchoolController(
        ISchoolService schoolService,
        ILogger<SchoolController> logger,
        ResponseHelper handler)
    {
        _schoolService = schoolService;
        _logger = logger;
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartamentas([FromBody] DepartamentasRequest request)
    {
        var departamentas = Departamentas.ConvertFromRequest(request);
        var response = await _schoolService.CreateDepartamentas(departamentas);

        if (!response)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaskaita([FromBody] PaskaitaRequest request)
    {
        var paskaita = Paskaita.ConvertFromRequest(request);
        var response = await _schoolService.CreatePaskaita(paskaita);

        if (!response)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentas([FromBody] StudentasRequest request)
    {
        var studentas = Studentas.ConvertFromRequest(request);
        var response = await _schoolService.CreateStudentas(studentas);

        if (!response)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> ShowDepartamentai()
    {
        var result = await _schoolService.ShowDepartamentai();

        // Convert to response object
        var response = result.Select(a => Departamentas.ConvertToResponse(a));

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitos()
    {
        var result = await _schoolService.ShowPaskaitos();
        var response = result.Select(a => Paskaita.ConvertToResponse(a));
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentai()
    {
        var result = await _schoolService.ShowStudentai();
        var response = result.Select(a => Studentas.ConvertToResponse(a));

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDepartamentasForStudentas(Guid DepartamentasId, Guid StudentasId)
    {
        bool response = await _schoolService.UpdateDepartamentasForStudentas(StudentasId: StudentasId, DepartamentasId: DepartamentasId);

        if (!response)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> AddPaskaitaToDepartamentas(Guid DepartamentasId, Guid PaskaitaId)
    {
        bool response = await _schoolService.UpdatePaskaitaForDepartamentas(PaskaitaId: PaskaitaId, DepartamentasId: DepartamentasId);

        if (!response)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> NewPaskaitaToDepartamentas(Guid DepartamentasId, PaskaitaRequest request)
    {
        Paskaita paskaita = Paskaita.ConvertFromRequest(request);

        bool response = await _schoolService.NewPaskaitaToDepartamentas(paskaita: paskaita, DepartamentasId: DepartamentasId);

        if (!response)
        {
            return Problem();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> NewStudentasToDepartamentas(Guid DepartamentasId, StudentasRequest request)
    {
        Studentas studentas = Studentas.ConvertFromRequest(request);

        bool response = await _schoolService.NewStudentasToDepartamentas(studentas: studentas, DepartamentasId: DepartamentasId);

        if (!response)
        {
            return Problem();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosForStudentas(Guid Id)
    {
        var result = await _schoolService.GetPaskaitosByStudentasId(StudentasId: Id);
        var response = result.Select(a => Paskaita.ConvertToResponse(a));

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosInDepartamentas(Guid Id)
    {
        var result = await _schoolService.GetPaskaitosInDepartamentas(DepartamentasId: Id);
        var response = result.Select(a => Paskaita.ConvertToResponse(a));

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentaiInDepartamentas(Guid Id)
    {
        var result = await _schoolService.GetStudentaiInDepartamentas(DepartamentasId: Id);
        var response = result.Select(a => Studentas.ConvertToResponse(a));

        return Ok(response);
    }
}
