using Geotab.Checkmate.ObjectModel;

namespace vehicle_backup.Entities
{
    public class APIResultData(IList<Device> devices, IList<DeviceStatusInfo> deviceStatusInfos, IList<Trip> trips)
    {
        public IList<Device> Devices { get; } = devices;

        public IList<DeviceStatusInfo> DeviceStatusInfos { get; } = deviceStatusInfos;

        public IList<Trip> Trips { get; } = trips;
    }
}