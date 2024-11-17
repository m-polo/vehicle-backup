namespace vehicle_backup.Interfaces
{
    public interface IExportService
    {
        void WriteItemsInfo<T>(IList<T> items) where T : IEntity;
    }
}