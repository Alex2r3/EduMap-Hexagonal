namespace Edumap.Infrastructure.Adapters.Security;

// Configuración JWT (sección "Jwt"); la clave de firma nunca va en appsettings.json, se carga desde user-secrets o variables de entorno
public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    // HMAC-SHA256 requiere una clave de al menos 256 bits
    public const int LongitudMinimaClave = 32;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public int ExpirationMinutes { get; set; } = 60;
}
