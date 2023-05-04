using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Domain.Repositories;

public interface IDateAndDurationRepository
{
    Task CreateAsync(DateAndDurationEntity dateAndDuration);
    Task UpdateAsync(DateAndDurationEntity dateAndDuration);
    Task DeleteAsync(Guid dateAndDurationId);
    Task<DateAndDurationEntity> GetByIdAsync(Guid dateAndDurationId);
    Task<List<DateAndDurationEntity>> ListAllAsync();
    Task<List<DateAndDurationEntity>> ListByStartDateAsync(DateTime startDate);
    Task<List<DateAndDurationEntity>> ListByTripAsync(Guid tripId);
    Task<List<DateAndDurationEntity>> ListByDurationAsync(int duration);
}
