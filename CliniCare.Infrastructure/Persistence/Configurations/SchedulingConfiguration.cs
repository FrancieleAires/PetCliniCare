using CliniCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Infrastructure.Persistence.Configurations
{
    public class SchedulingConfiguration : IEntityTypeConfiguration<Scheduling>
    {
        public void Configure(EntityTypeBuilder<Scheduling> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict) 
                .IsRequired();

            builder.HasOne(s => s.Animal)
                .WithMany()
                .HasForeignKey(s => s.AnimalId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(s => s.Veterinarian)
                .WithMany()
                .HasForeignKey(s => s.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(s => s.Procedure)
               .WithMany()
               .HasForeignKey(s => s.ProcedureId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
        }
    }
}
