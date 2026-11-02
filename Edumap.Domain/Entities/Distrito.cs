namespace Edumap.Domain.Entities;

// Catálogo: ubicación geográfica usada en los filtros de búsqueda
public class Distrito
{
    private Distrito() { }

    public int IdDistrito { get; private set; }

    public string Nombre { get; private set; } = null!;

    public string Provincia { get; private set; } = null!;

    public ICollection<Institucion> Instituciones { get; private set; } = new List<Institucion>();
}
