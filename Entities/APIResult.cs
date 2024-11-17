using vehicle_backup.Interfaces;

namespace vehicle_backup.Entities
{
    public class APIResult<T> : IResult<T>
    {
        public T Data { get; set; }
    }
}