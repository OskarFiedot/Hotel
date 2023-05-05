using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;
using Hotel.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public RoomTypeRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(RoomTypeEntity roomType)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        context.RoomTypes.Add(roomType);
        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roomTypeId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        var roomType = await GetByIdAsync(roomTypeId);

        if (roomType == null)
        {
            return;
        }

        context.RoomTypes.Remove(roomType);
        _ = await context.SaveChangesAsync();
    }

    public async Task<RoomTypeEntity> GetByIdAsync(Guid roomTypeId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == roomTypeId);
    }

    public async Task<List<RoomTypeEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.RoomTypes.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(RoomTypeEntity roomType)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.RoomTypes.Update(roomType);

        _ = await context.SaveChangesAsync();
    }
}
