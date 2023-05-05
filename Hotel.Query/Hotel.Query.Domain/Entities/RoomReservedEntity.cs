using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("RoomReserved")]
public class RoomReservedEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid ReservationId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual RoomEntity Room { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ReservationEntity Reservation { get; set; } = null!;
    public float Price { get; set; }
}
