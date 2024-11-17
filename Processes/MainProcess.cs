using vehicle_backup.Entities;
using vehicle_backup.Interfaces;
using vehicle_backup.Services;

namespace vehicle_backup.Processes
{
    public class MainProcess(string? directory, ILoggingService logger)
    {
        const string FILE_HEADERS = "Id,Name,VIN,Date,Longitude,Latitude,Odometer\r\n";
        private readonly ILoggingService _logger = logger;

        public async Task RunAsync(APIConfiguration apiParameters, CancellationTokenSource cancellationToken)
        {
            try
            {
                var exportService = new CSVService(FILE_HEADERS, directory, _logger);
                var cacheService = new CacheService(_logger);
                var mapperService = new MapperService(cacheService, _logger);
                var apiService = new APIService(apiParameters, cacheService, _logger);

                await apiService.AuthenticateAsync();

                await Task.Run(async () =>
                {
                    do
                    {
                        var feedProcess = new FeedProcess(apiService, mapperService, exportService, _logger);
                        await feedProcess.RunAsync();

                        Console.WriteLine("Waiting one minute for next execution");
                        await Task.Delay(60000);
                    }
                    while (true && !cancellationToken.IsCancellationRequested);
                }, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                _logger.LogText($"Error: {ex.Message}. StackTrace: {ex.StackTrace}");
            }
        }
    }
}
