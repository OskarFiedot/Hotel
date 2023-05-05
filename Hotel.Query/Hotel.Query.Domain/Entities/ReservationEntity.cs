using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Reservation")]
public class ReservationEntity
{
    [Key]
    public Guid Id { get; set; }
    public string User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public virtual List<RoomEntity> Rooms { get; } = new();
    public virtual List<RoomReservedEntity> RoomsReserved { get; } = new();
    public float TotalPrice { get; set; }
}
