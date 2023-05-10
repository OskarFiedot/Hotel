using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Reservation")]
public class ReservationEntity
{
    [Key]
    public Guid Id { get; set; }
    public string User { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public virtual List<RoomReservedEntity> RoomsReserved { get; } = new();
    public float TotalPrice { get; set; }
}
