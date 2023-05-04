using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("TripDate")]
public class TripDateEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public Guid DateAndDurationId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual TripEntity Trip { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual DateAndDurationEntity DateAndDuration { get; set; } = null!;
    public virtual List<ReservationEntity> Reservations { get; } = new();
    public int FreeSlots { get; set; }
    public float Price { get; set; }
}
