using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Venta.BlazorUI;
using Venta.BlazorUI.Services.Interface;
using Venta.BlazorUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7159/") });
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();

