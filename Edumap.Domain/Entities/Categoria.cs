namespace Edumap.Domain.Entities;

// Catálogo: tipo de institución (colegio, academia, instituto, universidad)
public class Categoria
{
    private Categoria() { }

    public int IdCategoria { get; private set; }

    public string Nombre { get; private set; } = null!;

    public string? IconoUrl { get; private set; }

    public ICollection<Institucion> Instituciones { get; private set; } = new List<Institucion>();
}
