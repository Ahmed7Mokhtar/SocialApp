using System.Reflection;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if(typeof(BaseEntity).IsAssignableTo(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property("Created")
                    .HasDefaultValueSql("GETUTCDATE()")
                    .IsRequired();
            }
        }
    }
}
