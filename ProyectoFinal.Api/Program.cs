using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProyectoFinal.Core.BusinessLogic;
using ProyectoFinal.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControlInventarioDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ControlInventarioDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControlInventario API", Version = "v1" });
});

builder.Services.AddCors(corsOptions => corsOptions.AddPolicy("ControlInventarioClient", policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:7127", "http://localhost:5055")));


// DI de Business 
builder.Services.AddScoped<IProductoBusiness, ProductoBusiness>();
builder.Services.AddScoped<ITipoProductoBusiness, TipoProductoBusiness>();
builder.Services.AddScoped<IInventarioBusiness, InventarioBusiness>();
builder.Services.AddScoped<IGastoBusiness, GastoBusiness>();
builder.Services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ControlInventarioClient");
app.MapControllers();

app.Run();