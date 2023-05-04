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
    public DbSet<TripEntity> Trips { get; set; }
    public DbSet<DateAndDurationEntity> DatesAndDurations { get; set; }
    public DbSet<PlaceOfDepartureEntity> PlacesOfDeparture { get; set; }
    public DbSet<TripDateEntity> TripDates { get; set; }
}
