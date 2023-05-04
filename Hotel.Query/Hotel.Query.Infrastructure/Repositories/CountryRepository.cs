using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public CountryRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(CountryEntity country)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Countries.Add(country);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid countryId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var country = await GetByIdAsync(countryId);

        if (country == null)
        {
            return;
        }

        context.Countries.Remove(country);
        _ = await context.SaveChangesAsync();
    }

    public async Task<CountryEntity> GetByIdAsync(Guid countryId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Countries
            .Include(c => c.Cities)
            .FirstOrDefaultAsync(c => c.Id == countryId);
    }

    public async Task<List<CountryEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Countries
            .AsNoTracking()
            .Include(c => c.Cities)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(CountryEntity country)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Countries.Update(country);

        _ = await context.SaveChangesAsync();
    }
}
