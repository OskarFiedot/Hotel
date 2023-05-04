using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Query.Domain.Entities;

[Table("Country")]
public class CountryEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<CityEntity> Cities { get; } = new();
}
