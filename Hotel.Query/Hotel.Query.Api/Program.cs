using Hotel.Query.Infrastructure.DataAccess;
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

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(
    new DatabaseContextFactory(configureDbContext)
);

// Create database and tables from code
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
