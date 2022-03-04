using eCommerce.Entities.Localization;

namespace eCommerce.Services.Services
{
    public interface ILocalizationService
    {
        LocaleStringResource GetStringResource(string resourceKey, int languageId);
    }
}
