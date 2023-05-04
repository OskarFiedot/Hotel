using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("City")]
public class CityEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CountryEntity Country { get; set; } = null!;
    public virtual List<TripEntity> Trips { get; } = new();
    public virtual List<PlaceOfDepartureEntity> Departures { get; } = new();
}
