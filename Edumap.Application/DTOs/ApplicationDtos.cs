namespace Edumap.Application.DTOs;

// DTOs: estructuras para transferir datos entre capas sin exponer las entidades de dominio
public record ApiResponseDto(bool Success, string Message);
