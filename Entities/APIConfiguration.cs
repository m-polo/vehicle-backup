namespace vehicle_backup.Entities
{
    public class APIConfiguration(string user, string password, string? session, string database, string server, int maxRetries = 3): IAPIConfiguration
    {
        public string User { get; } = user;
        public string Password { get; } = password;
        public string? Session { get; } = session;
        public string Database { get; } = database;
        public string Server { get; } = server;
        public int MaxRetries { get; } = maxRetries;
    }
}