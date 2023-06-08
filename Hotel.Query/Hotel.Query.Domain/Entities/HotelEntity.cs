using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Hotel")]
public class HotelEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CityEntity City { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual List<RoomEntity> Rooms { get; } = new();
}
