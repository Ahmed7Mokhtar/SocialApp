using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tables
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UsersAddresses");

            builder.Property(m => m.City).HasMaxLength(200);
            builder.Property(m => m.Country).HasMaxLength(200);

            builder.HasOne(m => m.User)
                .WithOne(d => d.Address)
                .HasForeignKey<UserAddress>(m => m.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
