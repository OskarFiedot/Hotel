using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Core.Entities;

namespace Hotel.Query.Domain.Entities;

[Table("City")]
public class CityEntity : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CountryEntity Country { get; set; } = null!;
    public virtual List<HotelEntity> Hotels { get; } = new();
}
