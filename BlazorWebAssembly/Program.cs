using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebAssembly.DTO;
using BlazorWebAssembly.Pages;
using BlazorWebAssembly;
using MudBlazor.Services;
//using BlazorWebAssembly.Servicos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.0.39:53011") });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
