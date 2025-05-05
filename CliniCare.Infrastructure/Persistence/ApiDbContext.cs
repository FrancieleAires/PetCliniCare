using CliniCare.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CliniCare.Infrastructure.Persistence
{
    public class ApiDbContext : IdentityDbContext<ApplicationUser, Role, int>
    {

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<VeterinaryCare> VeterinaryCares { get; set; }
        public DbSet<Procedure> VeterinaryProcedures { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }

        public DbSet<Role> Roles {  get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=bd_clinicare;Uid=root;Pwd=root;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);



        }





    }
}
