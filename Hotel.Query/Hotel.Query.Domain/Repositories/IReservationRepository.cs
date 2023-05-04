using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IReservationRepository
{
    Task CreateAsync(ReservationEntity reservation);
    Task UpdateAsync(ReservationEntity reservation);
    Task DeleteAsync(Guid reservationId);
    Task<ReservationEntity> GetByIdAsync(Guid reservationId);
    Task<List<ReservationEntity>> ListAllAsync();
    Task<List<ReservationEntity>> ListByUserAsync(string user);
    Task<List<ReservationEntity>> ListByTripAsync(Guid tripId);
}
