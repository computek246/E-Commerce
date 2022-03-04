using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Common.Installers
{
    public static class InstallerExtension
    {
        private static List<IInstaller> _installers;

        public static List<IInstaller> Installers
        {
            get => _installers ?? GetInstallers();
            set => _installers = value;
        }

        private static List<IInstaller> GetInstallers()
        {
            var types = new List<Type>();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (var dll in Directory.GetFiles(path ?? string.Empty, "*.dll"))
            {
                types.AddRange(Assembly.LoadFrom(dll).GetExportedTypes()
                    .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract));
            }

            Installers = types
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .OrderBy(e => e.Order)
                .ToList();

            return Installers;
        }


        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            Installers.ForEach(installer =>
            {
                installer.ConfigureServices(services, configuration);
            });
        }

        public static void ConfigurePipelineInAssembly(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Installers.ForEach(installer =>
            {
                installer.Configure(app, env);
            });
        }
    }
}
