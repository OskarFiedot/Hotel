using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public CityRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(CityEntity city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Cities.Add(city);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid cityId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var city = await GetByIdAsync(cityId);

        if (city == null)
        {
            return;
        }

        context.Cities.Remove(city);
        _ = await context.SaveChangesAsync();
    }

    public async Task<CityEntity> GetByIdAsync(Guid cityId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Cities
            .Include(c => c.Country)
            .Include(c => c.Hotels)
            .FirstOrDefaultAsync(c => c.Id == cityId);
    }

    public async Task<List<CityEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Cities
            .AsNoTracking()
            .Include(c => c.Country)
            .AsNoTracking()
            .Include(c => c.Hotels)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<CityEntity>> ListByCountryAsync(string country)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Cities
            .AsNoTracking()
            .Include(c => c.Country)
            .AsNoTracking()
            .Include(c => c.Hotels)
            .AsNoTracking()
            .Where(c => c.Country.Name.Contains(country))
            .ToListAsync();
    }

    public async Task UpdateAsync(CityEntity city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Cities.Update(city);

        _ = await context.SaveChangesAsync();
    }
}
