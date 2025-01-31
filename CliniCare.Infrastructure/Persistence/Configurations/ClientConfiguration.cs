using CliniCare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Infrastructure.Persistence.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .HasOne(c => c.ApplicationUser)
                .WithOne()
                .HasForeignKey<Client>(c => c.ApplicationUserId)
                .IsRequired();
        }
    }
}
