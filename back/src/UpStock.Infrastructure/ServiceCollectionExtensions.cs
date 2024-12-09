﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Marten;
using Microsoft.Extensions.Hosting;
using UpStock.Domain;

namespace UpStock.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Initialize(
        this IServiceCollection services,
        string? connectionString,
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.ConfigureStorage(connectionString, environment);

        return services;
    }

    private static IServiceCollection ConfigureStorage(
        this IServiceCollection services,
        string? connectionString,
        IHostEnvironment environment)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("No db connection string filled.");
        }

        services.AddMartenStore<IUpstockStore>(options =>
        {
            options.Connection(connectionString);
            options.DatabaseSchemaName = "main";
            if (!environment.IsDevelopment())
            {
                options.DisableNpgsqlLogging = true;
            }
            options.UseSystemTextJsonForSerialization();
            options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsString;
        });

        services.TryAddScoped<IUpstockRepository, MartenRepository>();

        return services;
    }
}