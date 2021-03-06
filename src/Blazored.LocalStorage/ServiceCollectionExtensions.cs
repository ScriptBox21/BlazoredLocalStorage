﻿using System;
using System.Diagnostics.CodeAnalysis;
using Blazored.LocalStorage.JsonConverters;
using Blazored.LocalStorage.Serialization;
using Blazored.LocalStorage.StorageOptions;
using Microsoft.Extensions.DependencyInjection;

namespace Blazored.LocalStorage
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazoredLocalStorage(this IServiceCollection services)
            => AddBlazoredLocalStorage(services, null);

        public static IServiceCollection AddBlazoredLocalStorage(this IServiceCollection services, Action<LocalStorageOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, BrowserStorageProvider>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<ISyncLocalStorageService, LocalStorageService>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}
