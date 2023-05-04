using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Trip")]
public class TripEntity
{
    [Key]
    public Guid Id { get; set; }
    public string HotelName { get; set; }
    public Guid CityId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual CityEntity City { get; set; } = null!;
    public virtual List<TripDateEntity> TripDates { get; } = new();
    public virtual List<PlaceOfDepartureEntity> PlacesOfDeparture { get; } = new();
    public float Price { get; set; }
}
