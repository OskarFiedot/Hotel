using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface ICountryRepository
{
    Task CreateAsync(CountryEntity country);
    Task UpdateAsync(CountryEntity country);
    Task DeleteAsync(Guid countryId);
    Task<CountryEntity> GetByIdAsync(Guid countryId);
    Task<List<CountryEntity>> ListAllAsync();
}
