using System;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Helper.ExtensionMethod
{
    public static class ServiceCollectionExtension
    {
        public static TService GetService<TService>(this IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<TService>();
        }

        public static TService GetService<TService>(
            this IServiceCollection services,
            Action<TService> action
        )
        {
            var service = services.BuildServiceProvider().GetRequiredService<TService>();
            action.Invoke(service);
            return service;
        }
    }
}