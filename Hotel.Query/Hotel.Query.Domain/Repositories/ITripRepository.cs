using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface ITripRepository
{
    Task CreateAsync(TripEntity trip);
    Task UpdateAsync(TripEntity trip);
    Task DeleteAsync(Guid tripId);
    Task<TripEntity> GetByIdAsync(Guid tripId);
    Task<List<TripEntity>> ListAllAsync();
    Task<List<TripEntity>> ListByCountryAsync(string country);
    Task<List<TripEntity>> ListByCityAsync(string city);
    Task<List<TripEntity>> ListByDatesAsync(DateTime startDate, int duration);
    Task<List<TripEntity>> ListByDatesAsync(
        DateTime earliestDateofDeparture,
        DateTime latestDayOfReturn
    );
    Task<List<TripEntity>> ListByPlaceOfDepartureAsync(string city);
    Task<List<TripEntity>> ListByPriceAsync(float minPrice, float maxPrice);
}
