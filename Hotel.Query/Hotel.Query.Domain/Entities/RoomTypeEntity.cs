using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("RoomType")]
public class RoomTypeEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfPeople { get; set; }
    public virtual List<RoomEntity> Rooms { get; } = new();
}
