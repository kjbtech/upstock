using Microsoft.AspNetCore.Cors.Infrastructure;

namespace UpStock.WebApi.Cors;

internal static class Configuration
{
    internal const string DefaultCORSPolicyName = "WebApp";

    public static IServiceCollection ConfigureMyCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection(typeof(AllowedOrigins).Name).Get<AllowedOrigins>();

        if (allowedOrigins is null ||
            !allowedOrigins.Any())
        {
            throw new InvalidOperationException("During CORS configuration, allowed origins have not been found.");
        }

        services.AddCors(options =>
        {
            var policy = new CorsPolicyBuilder();
            policy.WithOrigins([.. allowedOrigins])
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("*");
            options.AddPolicy(name: DefaultCORSPolicyName, policy.Build());
        });

        return services;
    }

    public static void UseMyCors(this IApplicationBuilder app)
    {
        app.UseCors(DefaultCORSPolicyName);
    }
}
