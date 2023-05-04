using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("DateAndDuration")]
public class DateAndDurationEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime DateOfDeparture { get; set; }
    public int Duration { get; set; }
    public virtual List<TripDateEntity> Trips { get; } = new();
}
