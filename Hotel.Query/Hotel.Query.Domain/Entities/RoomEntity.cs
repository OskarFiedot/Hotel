using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Room")]
public class RoomEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid HotelId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual HotelEntity Hotel { get; set; } = null!;
    public Guid RoomTypeId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual RoomTypeEntity RoomType { get; set; } = null!;
    public virtual List<ReservationEntity> Reservations { get; } = new();
    public virtual List<RoomReservedEntity> RoomsReserved { get; } = new();
    public float Price { get; set; }
}
