using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.Services;
using MinimalAPI.ViewModels;


#region Builder
var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Context 
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context") ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

// Controllers
builder.Services.AddControllersWithViews();

// Swagger Endpoints
builder.Services.AddEndpointsApiExplorer();

// Swagger Entrada de token
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira seu token JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// My Services
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UsuarioService>();

// Autenticação e Autorização JWT
var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("JWT Key não encontrado!");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.NoResult();
                context.Response.StatusCode = 401;
                return context.Response.WriteAsync("Falha de autenticação: " + context.Exception.Message);
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                return context.Response.WriteAsync("Token inválido ou perdido: " + context.ErrorDescription);
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();
#endregion

#region Cultura
var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new[] { defaultCulture },
    SupportedUICultures = new[] { defaultCulture }
};
#endregion

#region Rotas
// Default
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

// POST/login - Valida acesso e retorna token
app.MapPost("/login", async (Login login, LoginService loginService) =>
{
    if (login != null)
    {
        var token = await loginService.RealizaLogin(login);
        return token is null ?
            Results.Unauthorized() :
            Results.Ok(new LogadoViewModel
            {
                Email = login.Email,
                Token = token
            });
    }
    return Results.Unauthorized();

}).AllowAnonymous().WithTags("Login");

// Rotas Autenticadas - Acesso permitido apenas com token válido
// GET/usuarios - Retorna lista de usuários
app.MapGet("/usuario", async (UsuarioService usuarioService) =>
{
    var usuarios = await usuarioService.ListaUsuarios();
    return Results.Ok(usuarios);
}).RequireAuthorization().WithTags("Usuário");
#endregion

// Middlewares
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRequestLocalization(localizationOptions);

app.Run();