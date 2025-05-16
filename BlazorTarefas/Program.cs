using BlazorTarefas.Components;
using BlazorTarefas.Servicos;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using TarefasLibrary.Repositorio.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddHttpClient<UsuarioServico>(client =>
    {
        // a URL base da sua API, incluindo o “/”
        client.BaseAddress = new Uri("http://localhost:53011/");
    });

// idem para TarefaServico, se precisar
builder.Services
    .AddHttpClient<TarefaServico>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:53011/");
    });

builder.Services
    .AddHttpClient<EmpresaServico>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:53011/");
    });

// Registrar serviços da aplicação
//builder.Services.AddScoped<UsuarioServico>();
//builder.Services.AddScoped<TarefaServico>();
builder.Services.AddHttpContextAccessor();

// Add Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add DbContext
//string connectionString = "Data Source=tarefas.db";
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(connectionString));

// Add services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseSession();

// Initialize database with seed data - commented out for now to get the app running
// We'll implement this in a middleware or background service later
// InitSeed.InicializarBancoDeDados(app.Services).Wait();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
