using System.Reflection;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interceptors;
using Persistence.ValueGenerators;

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

        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(m => m.ClrType.IsSubclassOf(typeof(BaseEntity))))
        {
            // Adding the GuidStringValueGenerator value generator to the Id property of the BaseEntity
            var idProperty = entityType.FindProperty(nameof(BaseEntity.Id));
            if(idProperty != null && idProperty.ClrType == typeof(string))
                idProperty.SetValueGeneratorFactory((_, _) => new GuidStringValueGenerator());
        }

        //foreach (var entity in modelBuilder.Model.GetEntityTypes())
        //{
        //    if(typeof(BaseEntity).IsAssignableTo(entity.ClrType))
        //    {
        //        modelBuilder.Entity(entity.ClrType)
        //            .Property("Created")
        //            .HasDefaultValueSql("GETUTCDATE()")
        //            .IsRequired();
        //    }
        //}
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new BaseEntityInterceptor());

        base.OnConfiguring(optionsBuilder);
    }

    #region Overriding the SaveChangesAsync
    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(m => m.State == EntityState.Added))
    //    {
    //        if (entry.State == EntityState.Added)
    //        {
    //            if (string.IsNullOrWhiteSpace(entry.Entity.Id))
    //                entry.Entity.Id = Guid.NewGuid().ToString();

    //            entry.Entity.Created = DateTimeOffset.UtcNow;
    //        }
    //    }

    //    return base.SaveChangesAsync(cancellationToken);
    //}
    #endregion
}
