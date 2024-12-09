using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace UpStock.Workers.Hangfire;

/// <summary>
/// Activateur utilisé par Hangfire pour l'injection de dépendances.
/// </summary>
public sealed class HangfireActivator : JobActivator
{
    private readonly IServiceScopeFactory m_ServiceScopeFactory;

    /// <param name="serviceScopeFactory"><see cref="IServiceScopeFactory"/></param>
    public HangfireActivator(IServiceScopeFactory serviceScopeFactory)
    {
        m_ServiceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// <see cref="JobActivator.BeginScope(JobActivatorContext)"/>
    /// </summary>
    public override JobActivatorScope BeginScope(JobActivatorContext context)
    {
        return new ServiceJobActivatorScope(m_ServiceScopeFactory.CreateScope());
    }
}

/// <summary>
/// Implémentation spécifique pour <see cref="JobActivatorScope"/> afin de permettre le fonctionnement de la DI avec.NET en application console.
/// </summary>
internal sealed class ServiceJobActivatorScope : JobActivatorScope
{
    private readonly IServiceScope _serviceScope;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="serviceScope"><see cref="IServiceScope"/></param>
    public ServiceJobActivatorScope(IServiceScope serviceScope)
    {
        _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
    }

    /// <summary>
    /// Resolving using the current service scope instance variable.
    /// </summary>
    public override object Resolve(Type type)
    {
        return _serviceScope.ServiceProvider.GetService(type);
    }
}
