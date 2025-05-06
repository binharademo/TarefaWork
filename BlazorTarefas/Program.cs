using BlazorTarefas.Components;
using BlazorTarefas.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddHttpClient<UsuarioServico>(client =>
    {
        // a URL base da sua API, incluindo o �/�
        client.BaseAddress = new Uri("https://localhost:50504/");
    });

// idem para TarefaServico, se precisar
builder.Services
    .AddHttpClient<TarefaServico>(client =>
    {
        client.BaseAddress = new Uri("https://localhost:50504/");
    });

// Registrar servi�os da aplica��o
//builder.Services.AddScoped<UsuarioServico>();
//builder.Services.AddScoped<TarefaServico>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseSession();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
