using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IRoomReservedRepository
{
    Task CreateAsync(RoomReservedEntity roomReserved);
    Task UpdateAsync(RoomReservedEntity roomReserved);
    Task DeleteAsync(Guid roomReservedId);
    Task<RoomReservedEntity> GetByIdAsync(Guid roomReservedId);
    Task<List<RoomReservedEntity>> ListAllAsync();
    Task<List<RoomReservedEntity>> ListByRoomAsync(Guid roomId);
    Task<List<RoomReservedEntity>> ListByReservationAsync(Guid reservationId);
    Task<List<RoomReservedEntity>> ListByPriceAsync(float minPrice, float maxPrice);
}
