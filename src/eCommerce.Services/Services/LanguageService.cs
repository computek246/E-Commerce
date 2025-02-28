﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using eCommerce.Entities.Localization;
using eCommerce.Security.Context;

namespace eCommerce.Services.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDbContext _context;

        public LanguageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _context.Languages.ToList();
        }

        public Language GetLanguageByCulture(string culture)
        {
            return _context.Languages.FirstOrDefault(x =>
                x.LanguageCulture.Trim().ToLower() == culture.Trim().ToLower());
        }

        public CultureInfo[] GetCultures()
        {
            try
            {
                return _context.Languages.Where(e => e.IsActive).OrderBy(e => e.DisplayOrder)
                    .ToList().Select(x => new CultureInfo(x.LanguageCulture)).ToArray();
            }
            catch (Exception)
            {
                //ignore

                return Array.Empty<CultureInfo>();
            }
        }
    }
}
