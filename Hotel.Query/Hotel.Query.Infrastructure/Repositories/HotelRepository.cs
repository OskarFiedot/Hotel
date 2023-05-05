using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public HotelRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(HotelEntity hotel)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Hotels.Add(hotel);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid hotelId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var hotel = await GetByIdAsync(hotelId);

        if (hotel == null)
        {
            return;
        }

        context.Hotels.Remove(hotel);
        _ = await context.SaveChangesAsync();
    }

    public async Task<HotelEntity> GetByIdAsync(Guid hotelId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Hotels.Include(h => h.City).FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<List<HotelEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Hotels
            .AsNoTracking()
            .Include(h => h.City)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<HotelEntity>> ListByCityAsync(string city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Hotels
            .AsNoTracking()
            .Include(h => h.City)
            .AsNoTracking()
            .Where(h => h.City.Name.Contains(city))
            .ToListAsync();
    }

    public async Task<List<HotelEntity>> ListByCountryAsync(string country)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Hotels
            .AsNoTracking()
            .Include(h => h.City)
            .AsNoTracking()
            .Where(h => h.City.Country.Name.Contains(country))
            .ToListAsync();
    }

    public async Task UpdateAsync(HotelEntity hotel)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Hotels.Update(hotel);

        _ = await context.SaveChangesAsync();
    }
}
