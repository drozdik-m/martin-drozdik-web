using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MartinDrozdik.Data.Models.People;
using MartinDrozdik.Data.Models.Projects;
using MartinDrozdik.Data.Models.Technologies;
using MartinDrozdik.Models;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.DataPersistence.DbContexts
{
    public class AppDb : DbContext
    {
        // -------- PROJECTS --------
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<ProjectLogo> ProjectLogos { get; set; }
        public DbSet<ProjectOgImage> ProjectOgImages { get; set; }
        public DbSet<ProjectDeveloper> ProjectDevelopers { get; set; }
        public DbSet<ProjectHasTag> ProjectHasTags { get; set; }
        
        // -------- PEOPLE --------
        public DbSet<Person> People { get; set; }
        public DbSet<PersonProfileImage> PeopleProfileImages { get; set; }

        // -------- TECHNOLOGIES --------
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechnologyLogo> TechnologyLogos { get; set; }

        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
