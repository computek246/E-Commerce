using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Entities.Localization;
using eCommerce.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eCommerce.Security.Context
{
    public class SampleData
    {
        private static ILogger<SampleData> _logger;


        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            _logger = scope.ServiceProvider.GetRequiredService<ILogger<SampleData>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            try
            {
                _logger.LogDebug("Initialize..");
                await DatabaseMigrate(context);
                var canConnect = await context.Database.CanConnectAsync();
                if (canConnect)
                {
                    await AddLanguages(context);
                    await AddLocaleStringResources(context);
                }
                else
                {
                    _logger.LogError("Database not exist.");

                    throw new Exception("Database not exist.");
                }

                _logger.LogDebug("Database Ready.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error in Seed Data.");
            }
        }

        private static async Task DatabaseMigrate(DbContext context)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            var migrations = pendingMigrations.ToList();
            if (migrations.Any())
            {
                _logger.LogDebug($"apply migrations {string.Join(", ", migrations)}");
                await context.Database.MigrateAsync();
            }
        }

        private static async Task AddLanguages(ApplicationDbContext context)
        {
            _logger.LogDebug(nameof(AddLanguages));

            var list = new List<Language>
            {
                new()
                {
                    Name = "English",
                    LanguageCulture = "en-US",
                    DisplayOrder = 0,
                    FlagImageFileName = "en.png",
                    IsActive = true,
                    UniqueSeoCode = "en"
                },
                new()
                {
                    Name = "Arabic",
                    LanguageCulture = "ar-EG",
                    DisplayOrder = 1,
                    FlagImageFileName = "ar.png",
                    IsActive = true,
                    UniqueSeoCode = "ar",
                    Rtl = true
                },
                new()
                {
                    Name = "French",
                    LanguageCulture = "fr-FR",
                    DisplayOrder = 2,
                    FlagImageFileName = "fr.png",
                    IsActive = true,
                    UniqueSeoCode = "fr"
                },
                new()
                {
                    Name = "German",
                    LanguageCulture = "de-DE",
                    DisplayOrder = 3,
                    FlagImageFileName = "de.png",
                    IsActive = true,
                    UniqueSeoCode = "de"
                }
            };

            var entities = list.Where(x => !context.Languages.Select(e => e.LanguageCulture).Contains(x.LanguageCulture));
            await context.Languages.AddRangeAsync(entities);

            await context.SaveChangesAsync();
        }

        private static async Task AddLocaleStringResources(ApplicationDbContext context)
        {
            _logger.LogDebug(nameof(AddLocaleStringResources));

            var list = new List<LocaleStringResource>
            {
                new()
                {
                    LanguageId = 1,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Welcome"
                },
                new()
                {
                    LanguageId = 2,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Bienvenue"
                },
                new()
                {
                    LanguageId = 3,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Willkommen"
                },
                new()
                {
                    LanguageId = 4,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "أهلا بك"
                }
            };

            var entities = list.Where(x => !context.LocaleStringResources.Select(e => e.ResourceName).Contains(x.ResourceName));
            await context.LocaleStringResources.AddRangeAsync(entities);

            await context.SaveChangesAsync();
        }
        
    }
}