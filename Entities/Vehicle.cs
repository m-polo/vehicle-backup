using vehicle_backup.Interfaces;

namespace vehicle_backup.Entities
{
    public class Vehicle(string id, string? name, DateTime? date, string? vin, double? latitude, double? longitude, double? odometer) : IEntity
    {
        public string Id { get; set; } = id;
        public string? Name { get; set; } = name;
        public string? VIN { get; set; } = vin;
        public DateTime? Date { get; set; } = date;
        public double? Latitude { get; set; } = latitude;
        public double? Longitude { get; set; } = longitude;
        public double? Odometer { get; set; } = odometer;
    }
}