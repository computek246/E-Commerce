using System.Collections.Generic;
using System.Globalization;
using eCommerce.Entities.Localization;

namespace eCommerce.Services.Services
{
    public interface ILanguageService
    {
        IEnumerable<Language> GetLanguages();
        Language GetLanguageByCulture(string culture);
        CultureInfo[] GetCultures();
    }
}
