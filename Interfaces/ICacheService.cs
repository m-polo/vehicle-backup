using Geotab.Checkmate.ObjectModel;
using vehicle_backup.Entities;

namespace vehicle_backup.Interfaces
{
    public interface ICacheService
    {
        void CacheData(IList<Device>? devicesData, IList<DeviceStatusInfo>? deviceStatusInfosData, IList<Trip>? tripsData);
        public APIResultData GetCacheData();
    }
}