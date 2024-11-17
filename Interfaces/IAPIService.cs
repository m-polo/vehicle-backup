namespace vehicle_backup.Interfaces
{
    public interface IAPIService<T>
    {
        Task AuthenticateAsync();
        Task<IResult<T>> CallAPIAsync();
    }
}