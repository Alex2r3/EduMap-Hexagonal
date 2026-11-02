using MediatR;
using Microsoft.AspNetCore.Mvc;
using Edumap.Application.UseCases.Instituciones.Commands;
using Edumap.Application.UseCases.Instituciones.Queries;

namespace Edumap.Api.Controllers;

// Controladores (Adaptadores Primarios): reciben solicitudes HTTP y despachan Comandos/Consultas vía MediatR
[ApiController]
[Route("api/[controller]")]
public class InstitucionesController(ISender sender) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType<InstitucionDetailDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInstitucionByIdQuery(id), cancellationToken);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInstitucionCommand command, CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}
