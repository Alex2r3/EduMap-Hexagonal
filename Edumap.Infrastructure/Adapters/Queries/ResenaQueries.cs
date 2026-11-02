using Microsoft.EntityFrameworkCore;
using Edumap.Application.Ports;
using Edumap.Application.UseCases.Resenas.Queries;
using Edumap.Domain.Entities;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Adapters.Queries;

// Adaptador de Lectura (CQRS): proyecta las reseñas visibles a DTOs, de la más reciente a la más antigua
internal sealed class ResenaQueries(ApplicationDbContext context) : IResenaQueries
{
    public async Task<IReadOnlyList<ResenaDto>> GetVisiblesByInstitucionAsync(int idInstitucion, CancellationToken cancellationToken)
        => await context.Resenas
            .AsNoTracking()
            .Where(r => r.IdInstitucion == idInstitucion && r.Estado == Resena.EstadoVisible)
            .OrderByDescending(r => r.FechaCreacion)
            .Select(r => new ResenaDto(
                r.IdResena,
                r.Usuario.Nombres + " " + r.Usuario.Apellidos,
                r.Calificacion,
                r.Comentario,
                r.FechaCreacion))
            .ToListAsync(cancellationToken);
}
