using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ApiGastos", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153/"); // ProyectoFinal.Api
});

builder.Services.AddHttpClient("ApiCuentas", client =>
{
    client.BaseAddress = new Uri("https://localhost:7204/"); // ProyectoFinal.MinimalApi
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();