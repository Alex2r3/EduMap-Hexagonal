using Edumap.Application.Exceptions;
using Edumap.Domain.Exceptions;

namespace Edumap.Api.Middleware;

// Middleware: interceptor global de excepciones que traduce los errores de dominio/aplicación a códigos HTTP en formato JSON.
// Los errores inesperados se registran en el log y se responden con un mensaje genérico para no filtrar detalles internos.
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                DomainException or ValidationException => (StatusCodes.Status400BadRequest, ex.Message),
                NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                ConflictException => (StatusCodes.Status409Conflict, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                logger.LogError(ex, "Error no controlado en {Method} {Path}", context.Request.Method, context.Request.Path);

            if (context.Response.HasStarted)
                throw;

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { message });
        }
    }
}
