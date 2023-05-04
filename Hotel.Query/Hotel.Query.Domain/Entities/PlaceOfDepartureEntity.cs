using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("PlaceOfDeparture")]
public class PlaceOfDepartureEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public Guid TripId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CityEntity City { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual TripEntity Trip { get; set; } = null!;
    public float Price { get; set; }
}
