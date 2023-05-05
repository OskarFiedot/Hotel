using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IRoomTypeRepository
{
    Task CreateAsync(RoomTypeEntity roomType);
    Task UpdateAsync(RoomTypeEntity roomType);
    Task DeleteAsync(Guid roomTypeId);
    Task<RoomTypeEntity> GetByIdAsync(Guid roomTypeId);
    Task<List<RoomTypeEntity>> ListAllAsync();
}
