using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface ICityRepository
{
    Task CreateAsync(CityEntity city);
    Task UpdateAsync(CityEntity city);
    Task DeleteAsync(Guid cityId);
    Task<CityEntity> GetByIdAsync(Guid cityId);
    Task<List<CityEntity>> ListAllAsync();
    Task<List<CityEntity>> ListByCountryAsync(string country);
}
