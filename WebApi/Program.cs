using Domain.Interfaces.Generics;
using Domain.Interfaces.IAdvogado;
using Domain.Interfaces.IAtualizacao;
using Domain.Interfaces.ICliente;
using Domain.Interfaces.ICompromisso;
using Domain.Interfaces.IContato;
using Domain.Interfaces.IEndereco;
using Domain.Interfaces.IEspecialidade;
using Domain.Interfaces.IPolo;
using Domain.Interfaces.IProcesso;
using Entities.Entidades;
using Infra.Configuração;
using Infra.Repositório;
using Infra.Repositório.Generics;
using WebApi.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Controller.MethodsCommom;
using WebApi.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JustoApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {   new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                         ?? throw new Exception("DefaultConnection configuration is missing");

builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(connectionString));

//applicationuser esta HERDANDO identityuser****
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ContextBase>()
    .AddDefaultTokenProviders();
//token marcoratti
var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Chave Token Security Inválida!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    });

builder.Services.AddAuthorization(options =>
{
    // Política baseada em papel (role)
    options.AddRoleBasedPolicy("AdminOnly", "Admin");

    // Política baseada em papel e claim
    options.AddRoleAndClaimBasedPolicy("SuperAdminOnly", "Admin", "id", "Yann");

    // Política baseada em papel (role)
    options.AddRoleBasedPolicy("UserOnly", "User");

    // Política personalizada
    options.AddCustomPolicy("ExclusivePolicyOnly", context =>
    {
        return context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "Yann")
            || context.User.IsInRole("SuperAdmin");
    });
});


//Servico do token Marcoratti
builder.Services.AddScoped<ITokenService, TokenService>();

//interface e repositorio
builder.Services.AddSingleton(typeof(InterfaceGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddSingleton<InterfaceAdvogado, RepositorioAdvogado>();
builder.Services.AddSingleton<InterfaceAtualizacao, RepositorioAtualizacao>();
builder.Services.AddSingleton<InterfaceCliente, RepositorioCliente>();
builder.Services.AddSingleton<InterfaceCompromisso, RepositorioCompromisso>();
builder.Services.AddSingleton<InterfaceContato, RepositorioContato>();
builder.Services.AddSingleton<InterfaceEndereco, RepositorioEndereco>();
builder.Services.AddSingleton<InterfaceEspecialidade, RepositorioEspecialidade>();
builder.Services.AddSingleton<InterfacePolo, RepositorioPolo>();
builder.Services.AddSingleton<InterfaceProcesso, RepositorioProcesso>();




//email
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();

// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorOrigin");

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();







