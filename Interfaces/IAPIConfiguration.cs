namespace vehicle_backup.Entities
{
    public interface IAPIConfiguration
    {
        string User { get; }
        string Password { get; }
        string? Session { get; }
        string Database { get; }
        string Server { get; }
        int MaxRetries { get; }
    }
}