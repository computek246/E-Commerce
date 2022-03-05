using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using eCommerce.Common.Installers;
using eCommerce.Helper.ExtensionMethod;
using eCommerce.Security.Context;
using eCommerce.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Services.DI
{
    public class ServicesInstaller : IInstaller
    {
        public int Order => 5;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLocalization();
            services.AddControllersWithViews()
                .AddViewLocalization();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILocalizationService, LocalizationService>();

            services.GetService<ApplicationDbContext>(context =>
            {
                if (!context.Database.CanConnect()) return;

                var cultures = services.GetService<ILanguageService>().GetCultures();
                services.Configure<RequestLocalizationOptions>(options =>
                {
                    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault());
                    options.SupportedCultures = cultures;
                    options.SupportedUICultures = cultures;
                });

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();
        }
    }
}
