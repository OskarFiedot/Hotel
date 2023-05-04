using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class DateAndDurationRepository : IDateAndDurationRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public DateAndDurationRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(DateAndDurationEntity dateAndDuration)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.DatesAndDurations.Add(dateAndDuration);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid dateAndDurationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var dateAndDuration = await GetByIdAsync(dateAndDurationId);

        if (dateAndDuration == null)
        {
            return;
        }

        context.DatesAndDurations.Remove(dateAndDuration);
        _ = await context.SaveChangesAsync();
    }

    public async Task<DateAndDurationEntity> GetByIdAsync(Guid dateAndDurationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.DatesAndDurations.FirstOrDefaultAsync(
            dd => dd.Id == dateAndDurationId
        );
    }

    public async Task<List<DateAndDurationEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.DatesAndDurations.AsNoTracking().ToListAsync();
    }

    public async Task<List<DateAndDurationEntity>> ListByDurationAsync(int duration)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.DatesAndDurations
            .AsNoTracking()
            .Where(dd => dd.Duration == duration)
            .ToListAsync();
    }

    public async Task<List<DateAndDurationEntity>> ListByStartDateAsync(DateTime startDate)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.DatesAndDurations
            .AsNoTracking()
            .Where(dd => dd.DateOfDeparture == startDate)
            .ToListAsync();
    }

    public async Task<List<DateAndDurationEntity>> ListByTripAsync(Guid tripId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.DatesAndDurations
            .AsNoTracking()
            .Where(dd => dd.Trips.Any(t => t.Id == tripId))
            .ToListAsync();
    }

    public async Task UpdateAsync(DateAndDurationEntity dateAndDuration)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.DatesAndDurations.Update(dateAndDuration);

        _ = await context.SaveChangesAsync();
    }
}
