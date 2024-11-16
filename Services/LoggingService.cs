using System.Text.Json;
using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class LoggingService : ILoggingService
    {
        public void LogItem<T>(T item)
        {
            Console.WriteLine($"{typeof(T)}: {JsonSerializer.Serialize(item)}");
        }

        public void LogText(string text)
        {
            Console.WriteLine(text);
        }
    }
}