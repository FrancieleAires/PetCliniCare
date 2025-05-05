using CliniCare.Application.Abstractions;
using CliniCare.Domain.Interfaces;
using CliniCare.Domain.Models;
using CliniCare.Domain.Repositories;
using CliniCare.Infrastructure.Jwt;
using CliniCare.Infrastructure.Persistence;
using CliniCare.Infrastructure.Repository;
using CliniCare.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiDbContext>();
            services.AddScoped<ApiDbContext>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IVeterinarianRepository, VeterinarianRepository>();
            services.AddScoped<IVeterinaryCareRepository, VeterinaryCareRepository>();
            services.AddScoped<IVeterinaryProcedureRepository, VeterinaryProcedureRepository>();
            services.AddScoped<ISchedulingRepository, SchedulingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, Role>()
                    .AddRoles<Role>()
                    .AddEntityFrameworkStores<ApiDbContext>()
                    .AddDefaultTokenProviders();

            var JwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(JwtSettingsSection);


            services.AddScoped<IJwtService, JwtService>();

            var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret!);

            services.AddAuthentication(options =>
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ClientOnly", policy => policy.RequireRole("Client"));
            });
            return services;
        }
    }
}
