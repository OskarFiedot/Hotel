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
    Task<List<ReservationEntity>> ListByHotelAsync(Guid hotelId);
    Task<List<ReservationEntity>> ListByStartDateAsync(DateTime startDate);
    Task<List<ReservationEntity>> ListByEndDateAsync(DateTime endDate);
    Task<List<ReservationEntity>> ListByDateCreatedAsync(DateTime dateCreated);
    Task<List<ReservationEntity>> ListByDateUpdatedAsync(DateTime dateUpdated);
    Task<List<ReservationEntity>> ListByPriceAsync(float minPrice, float maxPrice);
}
