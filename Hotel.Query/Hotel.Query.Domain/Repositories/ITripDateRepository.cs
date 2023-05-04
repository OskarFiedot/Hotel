using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface ITripDateRepository
{
    Task CreateAsync(TripDateEntity tripDate);
    Task UpdateAsync(TripDateEntity tripDate);
    Task DeleteAsync(Guid tripDateId);
    Task<TripDateEntity> GetByIdAsync(Guid tripDateId);
    Task<List<TripDateEntity>> ListAllAsync();
    Task<List<TripDateEntity>> ListByTripAsync(Guid tripId);
    Task<List<TripDateEntity>> ListByFreeSlotsAsync(int FreeSlots);
}
