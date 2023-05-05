using Hotel.Query.Infrastructure.DataAccess;
using Hotel.Query.Infrastructure.Repositories;
using Hotel.Query.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;

string GetEnv(string envName)
{
    string envValue = Environment.GetEnvironmentVariable(envName);

    if (string.IsNullOrEmpty(envValue))
    {
        throw new Exception($"{envName} environment variable is not set.");
    }

    return envValue;
}

// PostgreSQL config
string postgresHost = GetEnv("POSTGRES_HOST");
string postgresUser = GetEnv("POSTGRES_USER");
string postgresPasswd = GetEnv("POSTGRES_PASSWORD");
string postgresDb = GetEnv("POSTGRES_DATABASE");

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = (
    o =>
        o.UseLazyLoadingProxies()
            .UseNpgsql(
                $"Host={postgresHost}; Database={postgresDb}; Username={postgresUser}; Password={postgresPasswd}"
            )
);

// Create database and tables from code
DatabaseContextFactory databaseContextFactory = new(configureDbContext);
using DatabaseContext databaseContext = databaseContextFactory.CreateDbContext();
databaseContext.Database.EnsureCreated();

CityRepository cityRepository = new(databaseContextFactory);
CountryRepository countryRepository = new(databaseContextFactory);
HotelRepository hotelRepository = new(databaseContextFactory);
ReservationRepository reservationRepository = new(databaseContextFactory);
RoomRepository roomRepository = new(databaseContextFactory);
RoomReservedRepository roomReservedRepository = new(databaseContextFactory);
RoomTypeRepository roomTypeRepository = new(databaseContextFactory);

var eventHandler = new Hotel.Query.Infrastructure.Handlers.EventHandler(
    reservationRepository,
    cityRepository,
    countryRepository,
    hotelRepository,
    roomRepository,
    roomTypeRepository,
    roomReservedRepository
);
