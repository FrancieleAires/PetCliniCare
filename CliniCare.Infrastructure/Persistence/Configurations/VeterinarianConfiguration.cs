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
    public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
    {
        public void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .HasOne(c => c.ApplicationUser)
                .WithOne()
                .HasForeignKey<Veterinarian>(c => c.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
