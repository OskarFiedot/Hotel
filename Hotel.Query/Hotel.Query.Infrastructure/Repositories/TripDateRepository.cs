using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class TripDateRepository : ITripDateRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public TripDateRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(TripDateEntity tripDate)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.TripDates.Add(tripDate);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid tripDateId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var tripDate = await GetByIdAsync(tripDateId);

        if (tripDate == null)
        {
            return;
        }

        context.TripDates.Remove(tripDate);
        _ = await context.SaveChangesAsync();
    }

    public async Task<TripDateEntity> GetByIdAsync(Guid tripDateId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.TripDates
            .Include(td => td.Trip)
            .Include(td => td.DateAndDuration)
            .Include(td => td.Reservations)
            .FirstOrDefaultAsync(td => td.Id == tripDateId);
    }

    public async Task<List<TripDateEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.TripDates
            .AsNoTracking()
            .Include(td => td.Trip)
            .AsNoTracking()
            .Include(td => td.DateAndDuration)
            .AsNoTracking()
            .Include(td => td.Reservations)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TripDateEntity>> ListByFreeSlotsAsync(int FreeSlots)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.TripDates
            .AsNoTracking()
            .Include(td => td.Trip)
            .AsNoTracking()
            .Include(td => td.DateAndDuration)
            .AsNoTracking()
            .Include(td => td.Reservations)
            .AsNoTracking()
            .Where(td => td.FreeSlots >= FreeSlots)
            .ToListAsync();
    }

    public async Task<List<TripDateEntity>> ListByTripAsync(Guid tripId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.TripDates
            .AsNoTracking()
            .Include(td => td.Trip)
            .AsNoTracking()
            .Include(td => td.DateAndDuration)
            .AsNoTracking()
            .Include(td => td.Reservations)
            .AsNoTracking()
            .Where(td => td.TripId == tripId)
            .ToListAsync();
    }

    public async Task UpdateAsync(TripDateEntity tripDate)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.TripDates.Update(tripDate);

        _ = await context.SaveChangesAsync();
    }
}
