using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortalGalaxy.Client;
using PortalGalaxy.Client.Auth;
using PortalGalaxy.Client.Proxy.Interfaces;
using PortalGalaxy.Client.Proxy.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<IUserProxy, UserProxy>();
builder.Services.AddScoped<IJsonProxy, JsonProxy>();
builder.Services.AddScoped<ICategoriaProxy, CategoriaProxy>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticacionService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
