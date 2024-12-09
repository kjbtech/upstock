namespace UpStock.Workers.Hangfire;

/// <summary>
/// Paramètres propres à Hangfire
/// </summary>
public class HangfireSettings
{
    /// <summary>
    /// La connection string utilisée par Hangfire pour le stockage.
    /// </summary>
    public string ConnectionString { get; set; } = null!;
}
