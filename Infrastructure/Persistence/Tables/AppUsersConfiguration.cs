using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Tables;

public class AppUsersConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(m => m.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()")
            .HasMaxLength(100);
        builder.Property(m => m.UserName).HasMaxLength(200);
    }
}
