namespace vehicle_backup.Interfaces
{
    public interface ILoggingService
    {
        void LogItem<T>(T item);
        void LogText(string text);
    }
}