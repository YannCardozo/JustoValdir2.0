using Blazor_Catalogo.Client.Auth;
using JustoFront;
using JustoFront.Services;
using JustoFront.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using MudBlazor.Services;
using JustoFront.Services.Email;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddScoped<IAuthorizeService, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>());

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
    provider => provider.GetRequiredService<TokenAuthenticationProvider>());

//builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//Services e Interfaces
builder.Services.AddScoped<IAdvogadoService, AdvogadoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProcessoService, ProcessoService>();
builder.Services.AddScoped<IPoloService, PoloService>();
//caso não esteja configurado isso NÃO CONSEGUIRÁ solicitar da API

//local
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7114/") });

//producao
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://justoadv.somee.com/") });




await builder.Build().RunAsync();

