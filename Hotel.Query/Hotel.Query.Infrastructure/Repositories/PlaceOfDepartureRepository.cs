using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class PlaceOfDepartureRepository : IPlaceOfDepartureRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public PlaceOfDepartureRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(PlaceOfDepartureEntity placeOfDeparture)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.PlacesOfDeparture.Add(placeOfDeparture);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid placeOfDepartureId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var placeOfDeparture = await GetByIdAsync(placeOfDepartureId);

        if (placeOfDeparture == null)
        {
            return;
        }

        context.PlacesOfDeparture.Remove(placeOfDeparture);
        _ = await context.SaveChangesAsync();
    }

    public async Task<PlaceOfDepartureEntity> GetByIdAsync(Guid placeOfDepartureId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.PlacesOfDeparture
            .Include(pd => pd.City)
            .Include(pd => pd.Trip)
            .FirstOrDefaultAsync(pd => pd.Id == placeOfDepartureId);
    }

    public async Task<List<PlaceOfDepartureEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.PlacesOfDeparture
            .AsNoTracking()
            .Include(pd => pd.City)
            .AsNoTracking()
            .Include(pd => pd.Trip)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PlaceOfDepartureEntity>> ListByCityAsync(string city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.PlacesOfDeparture
            .AsNoTracking()
            .Include(pd => pd.City)
            .AsNoTracking()
            .Include(pd => pd.Trip)
            .AsNoTracking()
            .Where(pd => pd.City.Name.Contains(city))
            .ToListAsync();
    }

    public async Task<List<PlaceOfDepartureEntity>> ListByTripAsync(Guid tripId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.PlacesOfDeparture
            .AsNoTracking()
            .Include(pd => pd.City)
            .AsNoTracking()
            .Include(pd => pd.Trip)
            .AsNoTracking()
            .Where(pd => pd.TripId == tripId)
            .ToListAsync();
    }

    public async Task UpdateAsync(PlaceOfDepartureEntity placeOfDeparture)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.PlacesOfDeparture.Update(placeOfDeparture);

        _ = await context.SaveChangesAsync();
    }
}
