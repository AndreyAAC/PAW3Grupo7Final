using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// DB context
builder.Services.AddDbContext<ControlInventarioDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ControlInventario")));

// DI de Business Logic
builder.Services.AddScoped<ICuentaPagarBusiness, CuentaPagarBusiness>();
builder.Services.AddScoped<ICitaBusiness, CitaBusiness>();


builder.Services.AddCors(o => o.AddPolicy("ControlInventarioClient", p => p
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:7127", "http://localhost:5055")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API - Cuentas por Pagar",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseHttpsRedirection();
app.UseCors("ControlInventarioClient");

// Cuentas pagar minimalAPI

var cuentas = app.MapGroup("/cuentas-pagar");

// GET /cuentas-pagar
cuentas.MapGet("/", async (ICuentaPagarBusiness business) =>
{
    var data = await business.GetAllAsync();
    return Results.Ok(data);
});

// GET /cuentas-pagar/{id}
cuentas.MapGet("/{id:int}", async (int id, ICuentaPagarBusiness business) =>
{
    var dto = await business.GetByIdAsync(id);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

// POST /cuentas-pagar
cuentas.MapPost("/", async (CuentaPagarDTO dto, ICuentaPagarBusiness business) =>
{
    var ok = await business.CreateAsync(dto);
    if (!ok) return Results.BadRequest(false);
    return Results.Ok(true);
});

// PUT /cuentas-pagar/{id}
cuentas.MapPut("/{id:int}", async (int id, CuentaPagarDTO dto, ICuentaPagarBusiness business) =>
{
    var ok = await business.UpdateAsync(id, dto);
    return ok ? Results.Ok(true) : Results.NotFound(false);
});

// DELETE /cuentas-pagar/{id}
cuentas.MapDelete("/{id:int}", async (int id, ICuentaPagarBusiness business) =>
{
    var ok = await business.DeleteAsync(id);
    return ok ? Results.Ok(true) : Results.NotFound(false);
});

// Historial de Citas - Minimal API

var citasHistorial = app.MapGroup("/citas-historial");

// GET /citas-historial
citasHistorial.MapGet("/", async (ICitaBusiness business) =>
{
    var data = await business.GetHistorialAsync();
    return Results.Ok(data);
});

// GET /citas-historial/{id}
citasHistorial.MapGet("/{id:int}", async (int id, ICitaBusiness business) =>
{
    var cita = await business.GetByIdAsync(id);
    if (cita == null)
        return Results.NotFound();

    return Results.Ok(cita);
});


app.Run();