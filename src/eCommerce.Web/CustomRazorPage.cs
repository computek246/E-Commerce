using System.Threading;
using eCommerce.Services.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace eCommerce.Web
{
    public abstract class CustomRazorPage<TModel> : RazorPage<TModel>
    {
        [RazorInject]
        public ILanguageService LanguageService { get; set; }

        [RazorInject]
        public ILocalizationService LocalizationService { get; set; }


        public delegate HtmlString Localizer(string key, params object[] args);


        private Localizer _localizer;
        public Localizer Localize
        {
            get
            {
                if (_localizer != null) return _localizer;
                var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
                var language = LanguageService.GetLanguageByCulture(currentCulture);
                if (language != null)
                {
                    _localizer = (resourceKey, args) =>
                    {
                        var stringResource = LocalizationService.GetStringResource(resourceKey, language.Id);

                        if (stringResource == null || string.IsNullOrEmpty(stringResource.ResourceValue))
                        {
                            return new HtmlString(resourceKey);
                        }

                        var value = args == null || args.Length == 0
                            ? stringResource.ResourceValue
                            : string.Format(stringResource.ResourceValue, args);

                        return new HtmlString(value);
                    };
                }

                return _localizer;
            }
        }

    }
}
