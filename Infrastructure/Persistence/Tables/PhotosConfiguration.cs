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
    public class PhotosConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos");

            builder.Property(m => m.Url).HasMaxLength(500);
            builder.Property(m => m.PublicId).HasMaxLength(500);
        }
    }
}
