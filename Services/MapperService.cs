using Geotab.Checkmate.ObjectModel;
using vehicle_backup.Entities;
using vehicle_backup.Interfaces;

namespace vehicle_backup.Services
{
    public class MapperService(ICacheService cache, ILoggingService logger) : IMapperService
    {
        private readonly ILoggingService _logger = logger;
        private readonly ICacheService _cache = cache;

        public IList<Interfaces.IEntity> MapDataToEntities(APIResultData apiResultData)
        {
            _logger.LogText("Transforming API data to vehicles");

            var vehicles = new List<Interfaces.IEntity>();
            var cacheData = _cache.GetCacheData();

            foreach (var device in apiResultData.Devices)
            {
                var deviceId = device.Id!.ToString()!;
                var deviceStatusInfo = apiResultData.DeviceStatusInfos.FirstOrDefault(dsi => dsi.Device!.Id!.ToString() == deviceId)
                        ?? cacheData.DeviceStatusInfos.First(dsi => dsi.Device!.Id!.ToString() == deviceId);
                var odometer = cacheData.Trips.Where(t => t.Device!.Id!.ToString() == deviceId).Sum(t => t.Distance);

                var vehicle = CreateObject(device, deviceStatusInfo, odometer);

                _logger.LogItem(vehicle);

                vehicles.Add(vehicle);
            }

            foreach (var deviceStatusInfo in apiResultData.DeviceStatusInfos)
            {
                var deviceId = deviceStatusInfo.Device?.Id!.ToString()!;

                var deviceAlreadyAdded = vehicles.Any(v => v.Id == deviceId);
                if (!deviceAlreadyAdded)
                {
                    var device = cacheData.Devices.First(d => d.Id!.ToString() == deviceId);
                    var odometer = cacheData.Trips.Where(t => t.Device!.Id!.ToString() == deviceId).Sum(t => t.Distance);

                    var vehicle = CreateObject(device, deviceStatusInfo, odometer);

                    _logger.LogItem(vehicle);

                    vehicles.Add(vehicle);
                }
            }

            foreach (var trip in apiResultData.Trips)
            {
                var deviceId = trip.Device?.Id!.ToString()!;

                var deviceAlreadyAdded = vehicles.Any(v => v.Id == deviceId);
                if (!deviceAlreadyAdded)
                {
                    var device = cacheData.Devices.First(d => d.Id!.ToString() == deviceId);
                    var deviceStatusInfo = cacheData.DeviceStatusInfos.First(dsi => dsi.Device!.Id!.ToString() == deviceId);
                    var odometer = apiResultData.Trips.Where(t => t.Device!.Id!.ToString() == deviceId).Sum(t => t.Distance);

                    var vehicle = CreateObject(device, deviceStatusInfo, odometer);

                    _logger.LogItem(vehicle);

                    vehicles.Add(vehicle);
                }
            }

            _logger.LogText("API data transformed");

            return vehicles;
        }

        private Vehicle CreateObject(Device device, DeviceStatusInfo deviceStatusInfo, double? odometer)
        {
            return new Vehicle(device.Id!.ToString()!, device.Name, deviceStatusInfo.DateTime,
                     (device as GoDevice)?.VehicleIdentificationNumber, deviceStatusInfo.Longitude, deviceStatusInfo.Latitude, odometer);
        }
    }
}