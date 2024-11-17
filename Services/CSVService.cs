using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class CSVService(string headers, string? directory, ILoggingService logger) : IExportService
    {
        private readonly string _headers = headers;
        private readonly string _directory = directory ?? "csv";
        private readonly ILoggingService _logger = logger;

        public void WriteItemsInfo<T>(IList<T> items) where T : IEntity
        {
            _logger.LogText("Writing items info to CSV");

            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            foreach (var item in items)
            {
                WriteItemInfo(item);
            }

            _logger.LogText("Items info written to CSV");
        }

        private void WriteItemInfo<T>(T item) where T : IEntity
        {
            var filePath = $"{_directory}/{item.Id}.csv";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, _headers);
            }

            File.AppendAllText(filePath, ConvertItemToCSV(item) + "\r\n");
        }

        private string ConvertItemToCSV<T>(T item) where T : IEntity
        {
            var result = "";
            foreach (var property in item!.GetType().GetProperties())
            {
                result += property.GetValue(item, null)?.ToString() + ",";
            }

            return result.Substring(0, result.Length - 2);
        }
    }
}