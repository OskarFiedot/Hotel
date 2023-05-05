using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IRoomRepository
{
    Task CreateAsync(RoomEntity room);
    Task UpdateAsync(RoomEntity room);
    Task DeleteAsync(Guid roomId);
    Task<RoomEntity> GetByIdAsync(Guid roomId);
    Task<List<RoomEntity>> ListAllAsync();
    Task<List<RoomEntity>> ListByHotelAsync(Guid hotelId);
    Task<List<RoomEntity>> ListByRoomTypeAsync(Guid roomTypeId);
    Task<List<RoomEntity>> ListByNumberOfPeopleAsync(int numberOfPeople);
    Task<List<RoomEntity>> ListByReservationAsync(Guid reservationId);
    Task<List<RoomEntity>> ListByPriceAsync(float minPrice, float maxPrice);
}
