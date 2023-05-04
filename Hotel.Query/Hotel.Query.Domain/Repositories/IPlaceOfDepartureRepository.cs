using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IPlaceOfDepartureRepository
{
    Task CreateAsync(PlaceOfDepartureEntity placeOfDeparture);
    Task UpdateAsync(PlaceOfDepartureEntity placeOfDeparture);
    Task DeleteAsync(Guid placeOfDepartureId);
    Task<PlaceOfDepartureEntity> GetByIdAsync(Guid placeOfDepartureId);
    Task<List<PlaceOfDepartureEntity>> ListAllAsync();
    Task<List<PlaceOfDepartureEntity>> ListByCityAsync(string city);
    Task<List<PlaceOfDepartureEntity>> ListByTripAsync(Guid tripId);
}
