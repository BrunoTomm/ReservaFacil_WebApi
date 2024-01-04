using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservaFacil.Dominio.Map;
using ReservaFacil.Dominio.Mediador.Handlers.CommandHandler.EventoHandlers;
using ReservaFacil.Dominio.Servicos.Interface;
using ReservaFacil.Dominio.Servicos.Servicos;
using ReservaFacil.Infra.Contextos;
using ReservaFacil.Infra.Interface;
using ReservaFacil.Infra.Repositorios;
using ReservaFacil.WebApi.Conversor;
using ReservaFacil.WebApi.Interface;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservaFacil - WebAPI", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
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
                new string[] { }
            }
        };
    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR"); // Defina sua cultura padrão aqui
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") }; // Adicione as culturas suportadas
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") }; // Adicione as culturas de interface suportadas
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextoReservaFacil>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IEventoConversor, EventoConversor>();
builder.Services.AddScoped<IReservaConversor, ReservaConversor>();
builder.Services.AddScoped<IUsuarioConversor, UsuarioConversor>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IRepositorioBase, RepositorioBase>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddMediatR(typeof(AtualizarEventoCommandHandler).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfile));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
//app.UseCors(x => x
//    .AllowAnyOrigin()
//       .AllowAnyMethod()
//          .AllowAnyHeader());

app.MapControllers();

app.Run();
