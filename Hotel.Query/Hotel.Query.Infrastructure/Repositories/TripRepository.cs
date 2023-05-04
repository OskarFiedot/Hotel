using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public TripRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(TripEntity trip)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Trips.Add(trip);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid tripId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var trip = await GetByIdAsync(tripId);

        if (trip == null)
        {
            return;
        }

        context.Trips.Remove(trip);
        _ = await context.SaveChangesAsync();
    }

    public async Task<TripEntity> GetByIdAsync(Guid tripId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .Include(t => t.City)
            .Include(t => t.TripDates)
            .Include(t => t.PlacesOfDeparture)
            .FirstOrDefaultAsync(t => t.Id == tripId);
    }

    public async Task<List<TripEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByCityAsync(string city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(t => t.City.Name.Contains(city))
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByCountryAsync(string country)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(t => t.City.Country.Name.Contains(country))
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByDatesAsync(DateTime startDate, int duration)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(
                t =>
                    t.TripDates.Any(
                        x =>
                            x.DateAndDuration.DateOfDeparture == startDate
                            && x.DateAndDuration.Duration == duration
                    )
            )
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByDatesAsync(
        DateTime earliestDateofDeparture,
        DateTime latestDayOfReturn
    )
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(
                t =>
                    t.TripDates.Any(
                        x =>
                            x.DateAndDuration.DateOfDeparture >= earliestDateofDeparture
                            && x.DateAndDuration.DateOfDeparture.AddDays(x.DateAndDuration.Duration)
                                <= latestDayOfReturn
                    )
            )
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByPlaceOfDepartureAsync(string city)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(t => t.PlacesOfDeparture.Any(x => x.City.Name.Contains(city)))
            .ToListAsync();
    }

    public async Task<List<TripEntity>> ListByPriceAsync(float minPrice, float maxPrice)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Trips
            .AsNoTracking()
            .Include(t => t.City)
            .AsNoTracking()
            .Include(t => t.TripDates)
            .AsNoTracking()
            .Include(t => t.PlacesOfDeparture)
            .AsNoTracking()
            .Where(t => t.Price >= minPrice && t.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task UpdateAsync(TripEntity trip)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Trips.Update(trip);

        _ = await context.SaveChangesAsync();
    }
}
