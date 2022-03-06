using System.Globalization;
using System.Linq;
using eCommerce.Security.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Web.DI
{
    public static class RequestLocalization
    {
        public static void UseRequestLocalization(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var cultures = dbContext.Languages.Where(e => e.IsActive)
                .OrderBy(e => e.DisplayOrder)
                .Select(e => e.LanguageCulture).AsEnumerable()
                .Select(e => new CultureInfo(e))
                .ToArray();

            builder.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault());
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
        }
    }
}
