using eCommerce.Common.Installers;
using eCommerce.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Services.DI
{
    public class ServicesInstaller : IInstaller
    {
        public int Order => 5;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
