namespace Edumap.Domain.Entities;

// Catálogo: roles de acceso al sistema
public class Rol
{
    private Rol() { }

    public int IdRol { get; private set; }

    public string Nombre { get; private set; } = null!;

    public string? Descripcion { get; private set; }

    public ICollection<Usuario> Usuarios { get; private set; } = new List<Usuario>();
}

// Nombres de rol tal como están registrados en la tabla rol
public static class Roles
{
    public const string Usuario = "USUARIO";
    public const string Representante = "REPRESENTANTE";
    public const string Administrador = "ADMINISTRADOR";
}
