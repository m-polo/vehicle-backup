namespace vehicle_backup.Interfaces
{
    public interface IAPIService
    {
        Task AuthenticateAsync();
        Task<IResult> CallAPIAsync();
    }
}