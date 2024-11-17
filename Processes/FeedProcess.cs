using vehicle_backup.Entities;
using vehicle_backup.Interfaces;
using vehicle_backup.Services;

namespace vehicle_backup.Processes
{
    public class FeedProcess(IAPIService<APIResultData> apiService, IMapperService mapperService, IExportService exportService, ILoggingService logger)
    {
        private readonly IAPIService<APIResultData> _apiService = apiService;
        private readonly IExportService _exportService = exportService;
        private readonly IMapperService _mapperService = mapperService;
        private readonly ILoggingService _logger = logger;

        public async Task RunAsync()
        {
            _logger.LogText("Feed process started");

            var resultData = await _apiService.CallAPIAsync();

            var vehicles = _mapperService.MapDataToEntities((APIResultData)resultData.Data);

            _logger.LogText($"Vehicles count: {vehicles.Count}");

            _exportService.WriteItemsInfo(vehicles);

            _logger.LogText("Feed process finished");
        }
    }
}