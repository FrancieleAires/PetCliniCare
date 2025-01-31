using CliniCare.Application.Abstractions;
using CliniCare.Application.Validators;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Jwt;
using CliniCare.Infrastructure.Persistence;
using CliniCare.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(AnimalValidator).Assembly);
builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddScoped<ApiDbContext>();
builder.Services.AddIdentity<ApplicationUser, Role>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(JwtSettingsSection);

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
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                context.Fail("Token inválido.");
                return;
            }

        }

    };
});

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
builder.Services.AddScoped<IVeterinaryCareRepository, VeterinaryCareRepository>();
builder.Services.AddScoped<IVeterinaryProcedureRepository, VeterinaryProcedureRepository>();
builder.Services.AddScoped<ISchedulingRepository, SchedulingRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
