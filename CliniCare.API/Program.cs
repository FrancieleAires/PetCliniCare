using CliniCare.Application.Abstractions;
using CliniCare.Application.Services.Implementations;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.Validators;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Jwt;
using CliniCare.Infrastructure.Persistence;
using CliniCare.Infrastructure.Repository;
using CliniCare.Infrastructure.Seeders;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer().AddFluentValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
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
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();


builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddScoped<ApiDbContext>();


builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
builder.Services.AddScoped<IVeterinaryCareRepository, VeterinaryCareRepository>();
builder.Services.AddScoped<IVeterinaryProcedureRepository, VeterinaryProcedureRepository>();
builder.Services.AddScoped<ISchedulingRepository, SchedulingRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IVeterinarianService, VeterinarianService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddIdentity<ApplicationUser, Role>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(JwtSettingsSection);


builder.Services.AddScoped<IJwtService, JwtService>();

var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Secret!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience
    };
    
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ClientOnly", policy => policy.RequireRole("Client"));
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await RoleSeeder.CreateRoles(serviceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
