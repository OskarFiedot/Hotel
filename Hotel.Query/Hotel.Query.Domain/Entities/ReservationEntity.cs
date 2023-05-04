using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Reservation")]
public class ReservationEntity
{
    [Key]
    public Guid Id { get; set; }
    public string User { get; set; }
    public Guid TripDateId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual TripDateEntity TripDate { get; set; } = null!;
    public float TotalPrice { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
