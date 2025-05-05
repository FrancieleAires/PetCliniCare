using CliniCare.API;
using CliniCare.Application.Services.Interfaces;
using CliniCare.Application.Validators;
using CliniCare.Infrastructure.Hubs;
using CliniCare.Infrastructure.Seeders;
using FluentValidation;
using FluentValidation.AspNetCore;
using CliniCare.Infrastructure;
using CliniCare.Application;
using CliniCare.API.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer().AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();

builder.Services.AddSignalR();
builder.Services.AddScoped<IApplicationUser, ApplicationUsers>();
builder.Services.AddHealthChecks();

builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddSwagger();


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

app.MapHub<HospitalizationHub>("/hospitalizationHub");
app.UseHealthChecks("/hc");

app.MapControllers();

app.Run();
