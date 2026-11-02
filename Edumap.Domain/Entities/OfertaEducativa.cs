namespace Edumap.Domain.Entities;

// Programa o nivel que ofrece una institución, con su pensión estimada
public class OfertaEducativa
{
    private OfertaEducativa() { }

    public int IdOferta { get; private set; }

    public int IdInstitucion { get; private set; }

    public int IdNivel { get; private set; }

    public string Nombre { get; private set; } = null!;

    public string Modalidad { get; private set; } = null!;

    public string? Descripcion { get; private set; }

    public short? DuracionMeses { get; private set; }

    public decimal? PensionMin { get; private set; }

    public decimal? PensionMax { get; private set; }

    public decimal? Matricula { get; private set; }

    public bool Activa { get; private set; }

    public Institucion Institucion { get; private set; } = null!;

    public NivelEducativo NivelEducativo { get; private set; } = null!;
}
