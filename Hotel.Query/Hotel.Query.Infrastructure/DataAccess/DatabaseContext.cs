using Hotel.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.Infrastructure.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options)
        : base(options) { }

    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<HotelEntity> Hotels { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<RoomTypeEntity> RoomTypes { get; set; }
    public DbSet<RoomReservedEntity> RoomReserved { get; set; }
}
