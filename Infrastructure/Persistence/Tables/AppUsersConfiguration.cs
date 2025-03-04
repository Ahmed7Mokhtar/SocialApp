using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Tables;

public class AppUsersConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(m => m.UserName).HasMaxLength(200);
        builder.Property(m => m.KnownAs).HasMaxLength(200);
        builder.Property(m => m.Introduction).HasMaxLength(2000);
        builder.Property(m => m.LookingFor).HasMaxLength(2000);
        builder.Property(m => m.Interests).HasMaxLength(2000);
    }
}
