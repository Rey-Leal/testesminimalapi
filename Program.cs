using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.Services;
using MinimalAPI.ViewModels;

#region Builder
var builder = WebApplication.CreateBuilder(args);

// Context 
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context") ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

// Controllers
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// My Services
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<UsuarioService>();

// Autenticação e Autorização JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidateIssuer = true,
            //ValidateAudience = true,
            ValidateLifetime = true,
            //ValidateIssuerSigningKey = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
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

}).WithTags("Login");

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