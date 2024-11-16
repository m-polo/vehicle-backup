using Geotab.Checkmate;
using Geotab.Checkmate.ObjectModel;
using vehicle_backup.Entities;
using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class APIService(APIConfiguration parameters, ICacheService cache, ILoggingService logger) : IAPIService
    {
        private readonly ILoggingService _logger = logger;
        private readonly ICacheService _cache = cache;
        private readonly int _maxRetries = parameters.MaxRetries;
        private readonly API _api = new(parameters.User, parameters.Password, parameters.Session, parameters.Database, parameters.Server);
        private long _deviceLastVersion = 0;
        private long _deviceStatusInfoLastVersion = 0;
        private long _tripsLastVersion = 0;

        public async Task AuthenticateAsync()
        {
            _logger.LogText("Authenticating");

            await _api.AuthenticateAsync();

            _logger.LogText("Authenticated");
        }

        public async Task<IResult> CallAPIAsync()
        {
            _logger.LogText("Retrieving data");

            var devices = await GetFeedRequestAsync<Device>(_deviceLastVersion);
            var deviceStatusInfos = await GetFeedRequestAsync<DeviceStatusInfo>(_deviceStatusInfoLastVersion);
            var trips = await GetFeedRequestAsync<Trip>(_tripsLastVersion);

            if (devices?.Data?.Count > 0 || deviceStatusInfos?.Data?.Count > 0 || trips?.Data?.Count > 0)
            {
                _logger.LogText("New data retrieved");
                UpdateAPIEntitiesLastVersions(devices?.ToVersion, deviceStatusInfos?.ToVersion, trips?.ToVersion);

                _cache.CacheData(devices?.Data, deviceStatusInfos?.Data, trips?.Data);
            }
            else
            {
                _logger.LogText("No data retrieved");
            }

            return new APIResult
            {
                Data = new APIResultData(devices?.Data ?? [], deviceStatusInfos?.Data ?? [], trips?.Data ?? [])
            };
        }

       
        private async Task<FeedResult<T>> GetFeedRequestAsync<T>(long fromVersion) where T : Entity
        {
            var retries = 0;
            while (retries < _maxRetries)
            {
                try
                {
                    _logger.LogText($"Calling GetFeed method for {typeof(T)} with fromVersion {fromVersion}");
                    return await _api.CallAsync<FeedResult<T>>("GetFeed", typeof(T), new { fromVersion });

                }
                catch (Exception ex)
                {
                    _logger.LogText($"Error: {ex.Message}. Retry: {retries + 1}");
                    _logger.LogText($"Retrying call in one second");
                    retries++;
                    await Task.Delay(1000);
                }
            }

            throw new Exception($"Maximum number of retries reached without success");
        }

        private void UpdateAPIEntitiesLastVersions(long? deviceLastVersion, long? deviceStatusInfoLastVersion, long? tripsLastVersion)
        {
            _logger.LogText("Updating API entities last versions");

            _deviceLastVersion = deviceLastVersion ?? 0;
            _deviceStatusInfoLastVersion = deviceStatusInfoLastVersion ?? 0;
            _tripsLastVersion = tripsLastVersion ?? 0;

            _logger.LogText("Updated API entities last versions");
        }
    }
}
