using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Persistence.Configurations;

public class PlatformConfig : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.Name)
         .HasMaxLength(150)
         .IsRequired();

        builder.Property(t => t.Publisher)
            .HasMaxLength(128)
        .IsRequired();

        builder.Property(t => t.Cost)
            .HasMaxLength(128)
            .IsRequired();




    }
}

