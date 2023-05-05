using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class RoomReservedRepository : IRoomReservedRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public RoomReservedRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(RoomReservedEntity roomReserved)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.RoomReserved.Add(roomReserved);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roomReservedId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var roomReserved = await GetByIdAsync(roomReservedId);

        if (roomReserved == null)
        {
            return;
        }

        context.RoomReserved.Remove(roomReserved);
        _ = await context.SaveChangesAsync();
    }

    public async Task<RoomReservedEntity> GetByIdAsync(Guid roomReservedId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomReserved
            .Include(rr => rr.Room)
            .Include(rr => rr.Reservation)
            .FirstOrDefaultAsync(rr => rr.Id == roomReservedId);
    }

    public async Task<List<RoomReservedEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomReserved
            .AsNoTracking()
            .Include(rr => rr.Room)
            .AsNoTracking()
            .Include(rr => rr.Reservation)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<RoomReservedEntity>> ListByPriceAsync(float minPrice, float maxPrice)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomReserved
            .AsNoTracking()
            .Include(rr => rr.Room)
            .AsNoTracking()
            .Include(rr => rr.Reservation)
            .AsNoTracking()
            .Where(rr => rr.Price >= minPrice && rr.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<List<RoomReservedEntity>> ListByRoomAsync(Guid roomId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomReserved
            .AsNoTracking()
            .Include(rr => rr.Room)
            .AsNoTracking()
            .Include(rr => rr.Reservation)
            .AsNoTracking()
            .Where(rr => rr.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<List<RoomReservedEntity>> ListByReservationAsync(Guid reservationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomReserved
            .AsNoTracking()
            .Include(rr => rr.Room)
            .AsNoTracking()
            .Include(rr => rr.Reservation)
            .AsNoTracking()
            .Where(rr => rr.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task UpdateAsync(RoomReservedEntity roomReserved)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.RoomReserved.Update(roomReserved);

        _ = await context.SaveChangesAsync();
    }
}
