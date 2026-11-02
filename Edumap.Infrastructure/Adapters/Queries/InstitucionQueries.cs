using Microsoft.EntityFrameworkCore;
using Edumap.Application.Ports;
using Edumap.Application.UseCases.Instituciones.Queries;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Adapters.Queries;

// Adaptador de Lectura (CQRS): proyecta instituciones a DTOs sin tracking; la calificación sale de vw_calificacion_institucion
internal sealed class InstitucionQueries(ApplicationDbContext context) : IInstitucionQueries
{
    public Task<InstitucionDetailDto?> GetByIdAsync(int idInstitucion, CancellationToken cancellationToken)
        => context.Instituciones
            .AsNoTracking()
            .Where(i => i.IdInstitucion == idInstitucion)
            .Select(i => new InstitucionDetailDto(
                i.IdInstitucion,
                i.Nombre,
                i.Categoria.Nombre,
                i.TipoGestion,
                i.Descripcion,
                i.Direccion,
                i.Distrito.Nombre,
                i.Distrito.Provincia,
                i.Latitud,
                i.Longitud,
                i.Telefono,
                i.Whatsapp,
                i.Email,
                i.SitioWeb,
                i.LogoUrl,
                i.Estado,
                context.VwCalificacionInstituciones
                    .Where(v => v.IdInstitucion == i.IdInstitucion)
                    .Select(v => v.PromedioCalificacion)
                    .FirstOrDefault(),
                context.VwCalificacionInstituciones
                    .Where(v => v.IdInstitucion == i.IdInstitucion)
                    .Select(v => v.TotalResenas)
                    .FirstOrDefault() ?? 0))
            .FirstOrDefaultAsync(cancellationToken);
}
