using vehicle_backup.Interfaces;

namespace vehicle_backup.Entities
{
    public class APIResult : IResult
    {
        public object Data { get; set; }
    }
}