using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssembly;
using MudBlazor.Services;
using BlazorWebAssembly.Servicos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<UsuarioServico>(client => { client.BaseAddress = new Uri("http://192.168.0.39:53011/"); });
builder.Services.AddHttpClient<TarefaServico>(client => { client.BaseAddress = new Uri("http://192.168.0.39:53011/"); });
builder.Services.AddHttpClient<EmpresaServico>(client => { client.BaseAddress = new Uri("http://192.168.0.39:53011/"); });
builder.Services.AddHttpClient<SetorServico>(client => { client.BaseAddress = new Uri("http://192.168.0.39:53011/"); });
builder.Services.AddHttpClient<ComentarioServico>(client => { client.BaseAddress = new Uri("http://192.168.0.39:53011/"); });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
