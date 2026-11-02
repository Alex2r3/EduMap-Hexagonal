namespace Edumap.Domain.Exceptions;

// Excepción de Dominio: se lanza cuando una operación viola una regla de negocio de las entidades
public class DomainException(string message) : Exception(message);
