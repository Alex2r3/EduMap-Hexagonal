using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Edumap.Application.UseCases.Instituciones.Commands;
using Edumap.Application.UseCases.Instituciones.Queries;

namespace Edumap.Api.Controllers;

// Controladores (Adaptadores Primarios): reciben solicitudes HTTP y despachan Comandos/Consultas vía MediatR
[ApiController]
[Route("api/[controller]")]
public class InstitucionesController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await sender.Send(new GetInstitucionByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateInstitucionCommand command)
    {
        var id = await sender.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}
