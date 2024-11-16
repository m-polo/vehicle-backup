using vehicle_backup.Entities;

namespace vehicle_backup.Interfaces
{
    public interface IMapperService
    {
        IList<IEntity> MapDataToEntities(APIResultData apiResultData);
    }
}