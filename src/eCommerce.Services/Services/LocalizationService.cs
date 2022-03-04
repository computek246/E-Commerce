using System.Linq;
using eCommerce.Entities.Localization;
using eCommerce.Security.Context;

namespace eCommerce.Services.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ApplicationDbContext _context;

        public LocalizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public LocaleStringResource GetStringResource(string resourceKey, int languageId)
        {
            return _context.LocaleStringResources.FirstOrDefault(x =>
                x.ResourceName.Trim().ToLower() == resourceKey.Trim().ToLower()
                && x.LanguageId == languageId);
        }
    }
}
