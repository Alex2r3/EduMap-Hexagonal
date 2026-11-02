using Edumap.Domain.Exceptions;

namespace Edumap.Domain.Common;

// Validaciones reutilizables para proteger las invariantes de las entidades
internal static class Guard
{
    public static string Required(string? value, string campo, int maxLength = int.MaxValue)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException($"El campo '{campo}' es obligatorio.");

        return MaxLength(value.Trim(), campo, maxLength);
    }

    public static string? Optional(string? value, string campo, int maxLength = int.MaxValue)
        => string.IsNullOrWhiteSpace(value) ? null : MaxLength(value.Trim(), campo, maxLength);

    private static string MaxLength(string value, string campo, int maxLength)
    {
        if (value.Length > maxLength)
            throw new DomainException($"El campo '{campo}' no puede superar {maxLength} caracteres.");

        return value;
    }
}
