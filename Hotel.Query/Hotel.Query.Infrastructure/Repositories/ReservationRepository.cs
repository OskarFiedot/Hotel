using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public ReservationRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(ReservationEntity reservation)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.Reservations.Add(reservation);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid reservationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var reservation = await GetByIdAsync(reservationId);

        if (reservation == null)
        {
            return;
        }

        context.Reservations.Remove(reservation);
        _ = await context.SaveChangesAsync();
    }

    public async Task<ReservationEntity> GetByIdAsync(Guid reservationId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<List<ReservationEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByEndDateAsync(DateOnly endDate)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.EndDate == endDate)
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByHotelAsync(Guid hotelId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.RoomsReserved.Any(r => r.Room.HotelId == hotelId))
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByPriceAsync(float minPrice, float maxPrice)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.TotalPrice >= minPrice && r.TotalPrice <= maxPrice)
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByRoomAsync(Guid roomId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.RoomsReserved.Any(r => r.RoomId == roomId))
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByStartDateAsync(DateOnly startDate)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.StartDate == startDate)
            .ToListAsync();
    }

    public async Task<List<ReservationEntity>> ListByUserAsync(string user)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.RoomsReserved)
            .ThenInclude(r => r.Room)
            .AsNoTracking()
            .Where(r => r.User.Contains(user))
            .ToListAsync();
    }

    public async Task UpdateAsync(ReservationEntity reservation)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Reservations.Update(reservation);

        _ = await context.SaveChangesAsync();
    }
}
