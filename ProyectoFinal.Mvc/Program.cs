using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Auth/Login";      // Regresa a login, si no se ha logeado
        options.AccessDeniedPath = "/Auth/Login";
    });


builder.Services.AddControllersWithViews();

// Cache + Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// ProyectoFinal.Api

builder.Services.AddHttpClient("ApiGastos", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/"); 
});

builder.Services.AddHttpClient("ApiProductos", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

builder.Services.AddHttpClient("ApiInventarios", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

builder.Services.AddHttpClient("ApiTiposProducto", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

builder.Services.AddHttpClient("ApiAuth", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

builder.Services.AddHttpClient("ApiUsuarios", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/");
});

// ProyectoFinal.MinimalApi

builder.Services.AddHttpClient("ApiCuentas", client =>
{
    client.BaseAddress = new Uri("https://localhost:7204/"); 
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();