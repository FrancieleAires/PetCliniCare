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
    public class VeterinaryCareConfiguration : IEntityTypeConfiguration<VeterinaryCare>
    {
        public void Configure(EntityTypeBuilder<VeterinaryCare> builder)
        {
            builder.HasKey(vc => vc.Id);

            builder.HasOne(vc => vc.Animal)
                .WithMany()
                .HasForeignKey(vc => vc.AnimalId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(vc => vc.Veterinarian)
                .WithMany()
                .HasForeignKey(vc => vc.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
