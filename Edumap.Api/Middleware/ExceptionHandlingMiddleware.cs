using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Edumap.Api.Middleware;

// Middleware: interceptor global de excepciones para estandarizar respuestas de error en formato JSON
public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new { message = ex.Message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
