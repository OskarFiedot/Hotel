using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IHotelRepository
{
    Task CreateAsync(HotelEntity hotel);
    Task UpdateAsync(HotelEntity hotel);
    Task DeleteAsync(Guid hotelId);
    Task<HotelEntity> GetByIdAsync(Guid hotelId);
    Task<List<HotelEntity>> ListAllAsync();
    Task<List<HotelEntity>> ListByCountryAsync(string country);
    Task<List<HotelEntity>> ListByCityAsync(string city);
}
