using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Core.Entities;

namespace Hotel.Query.Domain.Entities;

[Table("Hotel")]
public class HotelEntity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CityEntity City { get; set; } = null!;
    public virtual List<RoomEntity> Rooms { get; } = new();
}
