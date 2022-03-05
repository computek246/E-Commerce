using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Common.Models;
using eCommerce.Entities.Localization;
using eCommerce.Security.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eCommerce.Security.Context
{
    public class ApplicationDbContext
        : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LocaleStringResource> LocaleStringResources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // To Set MaxLength for all string Properties
            foreach (var property in builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                // skip property that have MaxLength
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }
        }

        public override int SaveChanges()
        {
            SetAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            SetAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAudit()
        {
            var entityEntries = ChangeTracker.Entries<Auditable>().ToList();
            foreach (var entry in entityEntries)
                switch (entry.State)
                {
                    case EntityState.Added:
                        OnAdded(entry);
                        break;
                    case EntityState.Modified:
                        OnModified(entry);
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        OnDeleted(entry);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private static void OnDeleted(EntityEntry<Auditable> entry)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsActive = false;
            entry.Entity.IsDeleted = true;
            OnModified(entry);
        }

        private static void OnModified(EntityEntry<Auditable> entry)
        {
            entry.Entity.ModifierId = 0;
            entry.Entity.LastModifiedDate = DateTime.Now;
        }

        private static void OnAdded(EntityEntry<Auditable> entry)
        {
            entry.Entity.IsActive = true;
            entry.Entity.IsDeleted = false;
            entry.Entity.CreatorId = 0;
            entry.Entity.CreationDate = DateTime.Now;
            OnModified(entry);
        }
    }
}