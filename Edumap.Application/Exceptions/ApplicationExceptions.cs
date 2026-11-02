namespace Edumap.Application.Exceptions;

// Excepciones de Aplicación: el adaptador primario (API) las traduce a códigos HTTP

// La solicitud es inválida o hace referencia a datos que no existen (400)
public class ValidationException(string message, Exception? innerException = null) : Exception(message, innerException);

// El recurso solicitado no existe (404)
public class NotFoundException(string message) : Exception(message);

// La operación choca con el estado actual de los datos, por ejemplo un duplicado (409)
public class ConflictException(string message, Exception? innerException = null) : Exception(message, innerException);
