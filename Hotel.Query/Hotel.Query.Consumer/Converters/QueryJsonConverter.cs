using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Queries;
using Hotel.Query.Consumer.Queries;

namespace Hotel.Query.Consumer.Converters;

public class QueryJsonConverter : JsonConverter<BaseQuery>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(BaseQuery));
    }

    public override BaseQuery Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
        }

        if (!doc.RootElement.TryGetProperty("Type", out var type))
        {
            throw new JsonException("Could not detect the Type discriminator property!");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDiscriminator switch
        {
            nameof(FindAllCitiesQuery)
                => JsonSerializer.Deserialize<FindAllCitiesQuery>(json, options),
            nameof(FindAllCountriesQuery)
                => JsonSerializer.Deserialize<FindAllCountriesQuery>(json, options),
            nameof(FindAllHotelsQuery)
                => JsonSerializer.Deserialize<FindAllHotelsQuery>(json, options),
            nameof(FindAllReservationsQuery)
                => JsonSerializer.Deserialize<FindAllReservationsQuery>(json, options),
            nameof(FindAllRoomsQuery)
                => JsonSerializer.Deserialize<FindAllRoomsQuery>(json, options),
            nameof(FindAllRoomsReservationsQuery)
                => JsonSerializer.Deserialize<FindAllRoomsReservationsQuery>(json, options),
            nameof(FindAllRoomTypesQuery)
                => JsonSerializer.Deserialize<FindAllRoomTypesQuery>(json, options),
            nameof(FindCitiesByCountryQuery)
                => JsonSerializer.Deserialize<FindCitiesByCountryQuery>(json, options),
            nameof(FindCityByIdQuery)
                => JsonSerializer.Deserialize<FindCityByIdQuery>(json, options),
            nameof(FindCountryByIdQuery)
                => JsonSerializer.Deserialize<FindCountryByIdQuery>(json, options),
            nameof(FindHotelByIdQuery)
                => JsonSerializer.Deserialize<FindHotelByIdQuery>(json, options),
            nameof(FindHotelsByCityQuery)
                => JsonSerializer.Deserialize<FindHotelsByCityQuery>(json, options),
            nameof(FindHotelsByCountryQuery)
                => JsonSerializer.Deserialize<FindHotelsByCountryQuery>(json, options),
            nameof(FindReservationByIdQuery)
                => JsonSerializer.Deserialize<FindReservationByIdQuery>(json, options),
            nameof(FindReservationsByEndDateQuery)
                => JsonSerializer.Deserialize<FindReservationsByEndDateQuery>(json, options),
            nameof(FindReservationsByHotelQuery)
                => JsonSerializer.Deserialize<FindReservationsByHotelQuery>(json, options),
            nameof(FindReservationsByPriceQuery)
                => JsonSerializer.Deserialize<FindReservationsByPriceQuery>(json, options),
            nameof(FindReservationsByStartDateQuery)
                => JsonSerializer.Deserialize<FindReservationsByStartDateQuery>(json, options),
            nameof(FindReservationsByUserQuery)
                => JsonSerializer.Deserialize<FindReservationsByUserQuery>(json, options),
            nameof(FindReservationsByRoomQuery)
                => JsonSerializer.Deserialize<FindReservationsByRoomQuery>(json, options),
            nameof(FindRoomByIdQuery)
                => JsonSerializer.Deserialize<FindRoomByIdQuery>(json, options),
            nameof(FindRoomReservationByIdQuery)
                => JsonSerializer.Deserialize<FindRoomReservationByIdQuery>(json, options),
            nameof(FindRoomsByHotelQuery)
                => JsonSerializer.Deserialize<FindRoomsByHotelQuery>(json, options),
            nameof(FindRoomsByNumberOfPeopleQuery)
                => JsonSerializer.Deserialize<FindRoomsByNumberOfPeopleQuery>(json, options),
            nameof(FindRoomsByPriceQuery)
                => JsonSerializer.Deserialize<FindRoomsByPriceQuery>(json, options),
            nameof(FindRoomsByReservationQuery)
                => JsonSerializer.Deserialize<FindRoomsByReservationQuery>(json, options),
            nameof(FindRoomsByTypeQuery)
                => JsonSerializer.Deserialize<FindRoomsByTypeQuery>(json, options),
            nameof(FindRoomTypeByIdQuery)
                => JsonSerializer.Deserialize<FindRoomTypeByIdQuery>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        BaseQuery value,
        JsonSerializerOptions options
    )
    {
        throw new NotImplementedException();
    }
}
