using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MartinDrozdik.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.DbContexts
{
    public class AppDb : DbContext
    {

        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }

        /// <summary>
        /// Update "last edit" date on changed entities
        /// </summary>
        void UpdateLastEditDate()
        {
            var changeSet = ChangeTracker.Entries<EntityBase>();

            foreach (var entry in changeSet?.Where(c => c.State == EntityState.Modified))
                entry.Entity.LastEditAt = DateTime.Now;
        }

        /// <summary>
        /// Called by all overloads of "SaveChanges".
        /// This method eliminates duplicate calles in all "SaveChanges" overloads.
        /// </summary>
        void SaveChangesOperations()
        {
            UpdateLastEditDate();
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            SaveChangesOperations();
            return base.SaveChanges();
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SaveChangesOperations();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesOperations();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
