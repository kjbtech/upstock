using Amazon.S3;

namespace UpStock.WebApi.Files;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFiles(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CloudFlareR2>();
        services.AddScoped<GetItemsFromFile>();
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonS3>();

        return services;
    }
}
