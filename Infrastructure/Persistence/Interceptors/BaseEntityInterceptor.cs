using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interceptors
{
    internal class BaseEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetBaseEntityProps(eventData.Context!);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SetBaseEntityProps(eventData.Context!);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public static void SetBaseEntityProps(DbContext context)
        {
            if(context is null)
                return;

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>().Where(m => m.State == EntityState.Added))
            {
                entry.Entity.Id ??= Guid.NewGuid().ToString();
                entry.Entity.Created = DateTimeOffset.UtcNow;
            }
        }
    }
}
