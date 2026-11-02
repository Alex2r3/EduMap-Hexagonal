using Edumap.Api.Configuration;
using Edumap.Api.Middleware;
using Edumap.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Composición: la API (adaptador primario) conecta la capa Application con los adaptadores de Infrastructure
builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
