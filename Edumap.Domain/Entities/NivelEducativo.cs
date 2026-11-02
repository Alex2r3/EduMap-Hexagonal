namespace Edumap.Domain.Entities;

// Catálogo: niveles educativos (inicial, primaria, secundaria, pregrado, etc.)
public class NivelEducativo
{
    private NivelEducativo() { }

    public int IdNivel { get; private set; }

    public string Nombre { get; private set; } = null!;

    public ICollection<OfertaEducativa> OfertasEducativas { get; private set; } = new List<OfertaEducativa>();
}
