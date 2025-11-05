using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProyectoFinal.Data.Models;
using ProyectoFinal.Models.DTOs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControlInventarioDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ControlInventarioDB")));

// CORS (ajusta orígenes según tu MVC)
builder.Services.AddCors(o => o.AddPolicy("ControlInventarioClient", p => p
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:7127", "http://localhost:5055")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API - Cuentas por Pagar", Version = "v1" });
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

// ===== Minimal API: CUENTAS POR PAGAR =====
var cuentas = app.MapGroup("/cuentas-pagar");

// GET /cuentas-pagar
cuentas.MapGet("/", async (ControlInventarioDBContext db) =>
{
    var data = await db.CuentasPorPagar
        .OrderByDescending(c => c.FechaCuentaPagar)
        .ThenByDescending(c => c.IdCuentaPagar)
        .Select(c => new
        {
            c.IdCuentaPagar,
            c.Motivo,
            c.FechaCuentaPagar,
            c.Descripcion,
            c.Monto,
            c.PlazoPagar
        })
        .ToListAsync();
    return Results.Ok(data);
});

// GET /cuentas-pagar/{id}
cuentas.MapGet("/{id:int}", async (int id, ControlInventarioDBContext db) =>
{
    var c = await db.CuentasPorPagar.FindAsync(id);
    return c is null ? Results.NotFound() : Results.Ok(c);
});

// POST /cuentas-pagar
cuentas.MapPost("/", async (ControlInventarioDBContext db, CuentaPagarDTO dto) =>
{
    var entity = new CuentasPagar
    {
        Motivo = dto.Motivo,
        FechaCuentaPagar = dto.FechaCuentaPagar,
        Descripcion = dto.Descripcion,
        Monto = dto.Monto,
        PlazoPagar = dto.PlazoPagar
    };
    db.CuentasPorPagar.Add(entity);
    await db.SaveChangesAsync();
    return Results.Ok(true);
});

// PUT /cuentas-pagar/{id}
cuentas.MapPut("/{id:int}", async (int id, ControlInventarioDBContext db, CuentaPagarDTO dto) =>
{
    var entity = await db.CuentasPorPagar.FindAsync(id);
    if (entity is null) return Results.NotFound(false);

    entity.Motivo = dto.Motivo;
    entity.FechaCuentaPagar = dto.FechaCuentaPagar;
    entity.Descripcion = dto.Descripcion;
    entity.Monto = dto.Monto;
    entity.PlazoPagar = dto.PlazoPagar;

    await db.SaveChangesAsync();
    return Results.Ok(true);
});

// DELETE /cuentas-pagar/{id}
cuentas.MapDelete("/{id:int}", async (int id, ControlInventarioDBContext db) =>
{
    var entity = await db.CuentasPorPagar.FindAsync(id);
    if (entity is null) return Results.NotFound(false);

    db.CuentasPorPagar.Remove(entity);
    await db.SaveChangesAsync();
    return Results.Ok(true);
});

app.Run();