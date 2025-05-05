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
    public class HospitalizationConfiguration : IEntityTypeConfiguration<Hospitalization>
    {
        public void Configure(EntityTypeBuilder<Hospitalization> builder)
        {
            builder.HasKey(h => h.Id);

            builder.HasOne(i => i.Animal)
            .WithMany(a => a.Hospitalizations)
            .HasForeignKey(i => i.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
