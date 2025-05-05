using CliniCare.Application.Abstractions;
using CliniCare.Application.Commands.Animals.InsertAnimal;
using CliniCare.Application.Services.Implementations;
using CliniCare.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IVeterinarianService, VeterinarianService>();

            services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(InsertAnimalCommand).Assembly));

            return services;
        }
    }
}
