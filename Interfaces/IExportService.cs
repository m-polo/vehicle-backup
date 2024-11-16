namespace vehicle_backup.Interfaces
{
    public interface IExportService
    {
        void WriteItemsInfo<T>(IList<T> items, string directory) where T : IEntity;
        void WriteItemInfo<T>(T item, string directory) where T : IEntity;
    }
}