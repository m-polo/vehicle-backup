using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class CSVService(string headers, ILoggingService logger) : IExportService
    {
        private readonly string _headers = headers;
        private readonly ILoggingService _logger = logger;

        public void WriteItemsInfo<T>(IList<T> items, string directory)  where T : IEntity
        {
            _logger.LogText("Writing items info to CSV");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            foreach (var item in items)
            {
                WriteItemInfo(item, directory);
            }

            _logger.LogText("Items info written to CSV");
        }

        public void WriteItemInfo<T>(T item, string directory) where T : IEntity
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = $"{directory}/{item.Id}.csv";

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