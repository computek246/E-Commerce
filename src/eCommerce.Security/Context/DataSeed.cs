using System.Collections.Generic;
using eCommerce.Entities.Localization;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Security.Context
{
    public static class DataSeed
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(new List<Language>
            {
                new()
                {
                    Id = 1,
                    Name = "English",
                    LanguageCulture = "en-US",
                    DisplayOrder = 0,
                    FlagImageFileName = "en.png",
                    IsActive = true,
                    UniqueSeoCode = "en"
                },
                new()
                {
                    Id = 2,
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
                    Id = 3,
                    Name = "French",
                    LanguageCulture = "fr-FR",
                    DisplayOrder = 2,
                    FlagImageFileName = "fr.png",
                    IsActive = true,
                    UniqueSeoCode = "fr"
                },
                new()
                {
                    Id = 4,
                    Name = "German",
                    LanguageCulture = "de-DE",
                    DisplayOrder = 3,
                    FlagImageFileName = "de.png",
                    IsActive = true,
                    UniqueSeoCode = "de"
                }
            });

            modelBuilder.Entity<LocaleStringResource>().HasData(new List<LocaleStringResource>
            {
                new()
                {
                    Id = 1,
                    LanguageId = 1,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Welcome"
                },
                new()
                {
                    Id = 2,
                    LanguageId = 2,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Bienvenue"
                },
                new()
                {
                    Id = 3,
                    LanguageId = 3,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "Willkommen"
                },
                new()
                {
                    Id = 4,
                    LanguageId = 4,
                    ResourceName = "home.index.welcome",
                    ResourceValue = "أهلا بك"
                }
            });

            return modelBuilder;
        }
    }
}