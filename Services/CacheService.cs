using Geotab.Checkmate.ObjectModel;
using vehicle_backup.Entities;
using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class CacheService(ILoggingService logger) : ICacheService
    {
        private readonly ILoggingService _logger = logger;
        private readonly IList<Device> _devicesCache = [];
        private readonly IList<DeviceStatusInfo> _devicesStatusInfosCache = [];
        private readonly IList<Trip> _tripsCache = [];

        public void CacheData(IList<Device>? devicesData, IList<DeviceStatusInfo>? deviceStatusInfosData, IList<Trip>? tripsData)
        {
            _logger.LogText("Caching device data");

            if (devicesData is not null && devicesData.Count != 0)
            {
                foreach (var device in devicesData)
                {
                    var deviceCache = _devicesCache.Where(d => d.Id?.ToString() == device.Id?.ToString()).FirstOrDefault();
                    if (deviceCache is not null)
                    {
                        deviceCache = device;
                    }
                    else
                    {
                        _devicesCache.Add(device);
                    }
                }
            }

            _logger.LogText("Device data cached");

            _logger.LogText("Caching device status info data");

            if (deviceStatusInfosData is not null && deviceStatusInfosData.Count != 0)
            {
                foreach (var deviceStatusInfo in deviceStatusInfosData)
                {
                    var deviceStatusInfoCache = _devicesStatusInfosCache.Where(d => d.Device?.Id?.ToString() == deviceStatusInfo.Id?.ToString()).FirstOrDefault();
                    if (deviceStatusInfoCache is not null)
                    {
                        deviceStatusInfoCache = deviceStatusInfo;
                    }
                    else
                    {
                        _devicesStatusInfosCache.Add(deviceStatusInfo);
                    }
                }
            }

            _logger.LogText("Device status info data cached");

            _logger.LogText("Caching trips data");

            if (tripsData is not null && tripsData.Count != 0)
            {
                foreach (var trip in tripsData)
                {
                    var tripCache = _tripsCache.Where(d => d.Id?.ToString() == trip.Id?.ToString()).FirstOrDefault();
                    if (tripCache is not null)
                    {
                        tripCache = trip;
                    }
                    else
                    {
                        _tripsCache.Add(trip);
                    }
                }
            }

            _logger.LogText("Trips data cached");
        }

        public APIResultData GetCacheData()
        {
            return new APIResultData(_devicesCache, _devicesStatusInfosCache, _tripsCache);
        }
    }
}