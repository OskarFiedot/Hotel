using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public RoomRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(RoomEntity room)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Rooms.Add(room);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roomId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var room = await GetByIdAsync(roomId);

        if (room == null)
        {
            return;
        }

        context.Rooms.Remove(room);
        _ = await context.SaveChangesAsync();
    }

    public async Task<RoomEntity> GetByIdAsync(Guid roomId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .Include(r => r.Hotel)
            .Include(r => r.RoomType)
            .FirstOrDefaultAsync(r => r.Id == roomId);
    }

    public async Task<List<RoomEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<RoomEntity>> ListByHotelAsync(Guid hotelId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<List<RoomEntity>> ListByNumberOfPeopleAsync(int numberOfPeople)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .Where(r => r.RoomType.NumberOfPeople == numberOfPeople)
            .ToListAsync();
    }

    public async Task<List<RoomEntity>> ListByPriceAsync(float minPrice, float maxPrice)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .Where(r => r.Price >= minPrice && r.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<List<RoomEntity>> ListByReservationAsync(Guid reservationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .Where(r => r.Reservations.Any(r => r.Id == reservationId))
            .ToListAsync();
    }

    public async Task<List<RoomEntity>> ListByRoomTypeAsync(Guid roomTypeId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .AsNoTracking()
            .Include(r => r.RoomType)
            .AsNoTracking()
            .Where(r => r.RoomTypeId == roomTypeId)
            .ToListAsync();
    }

    public async Task UpdateAsync(RoomEntity room)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Rooms.Update(room);

        _ = await context.SaveChangesAsync();
    }
}
