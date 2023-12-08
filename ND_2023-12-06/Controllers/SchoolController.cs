﻿using Microsoft.AspNetCore.Mvc;
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
        return await _handler.HandleErrors(async () => 
        {
            var departamentas = Departamentas.ConvertFromRequest(request);
            var response = await _schoolService.CreateDepartamentas(departamentas);

            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaskaita([FromBody] PaskaitaRequest request)
    {
        return await _handler.HandleErrors(async () =>
        {
            var paskaita = Paskaita.ConvertFromRequest(request);
            var response = await _schoolService.CreatePaskaita(paskaita);

            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentas([FromBody] StudentasRequest request)
    {
        return await _handler.HandleErrors(async () =>
        {
            var studentas = Studentas.ConvertFromRequest(request);

            var response = await _schoolService.CreateStudentas(studentas);

            if (!response)
            {
                return BadRequest();
            }

            return Ok();
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowDepartamentai()
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.ShowDepartamentai();

            // Convert to response object
            var response = result.Select(a => Departamentas.ConvertToResponse(a));

            return Ok(response);
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitos()
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.ShowPaskaitos();
            var response = result.Select(a => Paskaita.ConvertToResponse(a));
            return Ok(response);
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentai()
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.ShowStudentai();
            var response = result.Select(a => Studentas.ConvertToResponse(a));

            return Ok(response);
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDepartamentasForStudentas(Guid DepartamentasId, Guid StudentasId)
    {
        return await _handler.HandleErrors(async () =>
        {
            bool response = await _schoolService.UpdateDepartamentasForStudentas(StudentasId, DepartamentasId);

            if (!response)
            {
                return NotFound();
            }

            return Ok();
        });
    }

    [HttpPut]
    public async Task<IActionResult> AddPaskaitaToDepartamentas(Guid DepartamentasId, Guid PaskaitaId)
    {
        return await _handler.HandleErrors(async () =>
        {
            bool response = await _schoolService.UpdatePaskaitaForDepartamentas(PaskaitaId, DepartamentasId);

            if (!response)
            {
                return NotFound();
            }

            return Ok();
        });
    }

    [HttpPost]
    public async Task<IActionResult> NewPaskaitaToDepartamentas(Guid DepartamentasId, PaskaitaRequest request)
    {
        Paskaita paskaita = Paskaita.ConvertFromRequest(request);

        return await _handler.HandleErrors(async () =>
        {
            bool response = await _schoolService.NewPaskaitaToDepartamentas(paskaita, DepartamentasId);

            if (!response)
            {
                return Problem();
            }

            return NoContent();
        });
    }

    [HttpPost]
    public async Task<IActionResult> NewStudentasToDepartamentas(Guid DepartamentasId, StudentasRequest request)
    {
        Studentas studentas = Studentas.ConvertFromRequest(request);

        return await _handler.HandleErrors(async () =>
        {
            bool response = await _schoolService.NewStudentasToDepartamentas(studentas, DepartamentasId);

            if (!response)
            {
                return Problem();
            }

            return NoContent();
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosForStudentas(Guid StudentasId)
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.GetPaskaitosByStudentasId(StudentasId);
            var response = result.Select(a => Paskaita.ConvertToResponse(a));

            return Ok(response);
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowPaskaitosInDepartamentas(Guid DepartamentasId)
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.GetPaskaitosInDepartamentas(DepartamentasId);
            var response = result.Select(a => Paskaita.ConvertToResponse(a));

            return Ok(response);
        });
    }

    [HttpGet]
    public async Task<IActionResult> ShowStudentaiInDepartamentas(Guid DepartamentasId)
    {
        return await _handler.HandleErrors(async () =>
        {
            var result = await _schoolService.GetStudentaiInDepartamentas(DepartamentasId);
            var response = result.Select(a => Studentas.ConvertToResponse(a));

            return Ok(response);
        });
    }
}
