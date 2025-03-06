using BookStore.Application.Converter;
using BookStore.Application.Interface;
using BookStore.Application.Mappings;
using BookStore.Application.Services;
using BookStore.Domain.Handler;
using BookStore.Infrastructure.Contexts;
using BookStore.Infrastructure.Interfaces;
using BookStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
            ValidAudience = builder.Configuration["JWTConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"),
    x => x.MigrationsAssembly("BookStore.API").ToString())
    .EnableSensitiveDataLogging());

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateTimeJsonConverter());
});
builder.Services.AddScoped<PasswordHandler>(); 
builder.Services.AddScoped<IUsuarioResponseRepository, UsuarioRepositoryResponse>();
builder.Services.AddScoped<IUsuarioRepositoryRequest, UsuarioRepositoryRequest>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IAutoresRepository, AutoresRepository>();
builder.Services.AddScoped<IAutoresService, AutoresService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivrosService>();
builder.Services.AddScoped<IUsuarioResponseService, UsuarioResponseService>();
builder.Services.AddScoped<IUsuarioRequestService, UsuarioRequestService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();