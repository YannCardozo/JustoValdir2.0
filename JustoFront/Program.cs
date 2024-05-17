using Blazor_Catalogo.Client.Auth;
using JustoFront;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddScoped<IAuthorizeService, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>());

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>());

//builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAdvogadoService, AdvogadoService>();


//caso não esteja configurado isso NÃO CONSEGUIRÁ solicitar da API
//Utilizar em casa
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7114/") });




await builder.Build().RunAsync();

