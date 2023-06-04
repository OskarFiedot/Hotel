using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Consumer.Queries;

public interface IQueryHandler
{
    Task<List<HotelEntity>> HandleAsync(FindAllHotelsQuery query);
    Task<List<HotelEntity>> HandleAsync(FindHotelByIdQuery query);
    Task<List<HotelEntity>> HandleAsync(FindHotelsByCityQuery query);
    Task<List<HotelEntity>> HandleAsync(FindHotelsByCountryQuery query);
    Task<List<CityEntity>> HandleAsync(FindAllCitiesQuery query);
    Task<List<CityEntity>> HandleAsync(FindCityByIdQuery query);
    Task<List<CityEntity>> HandleAsync(FindCitiesByCountryQuery query);
    Task<List<CountryEntity>> HandleAsync(FindAllCountriesQuery query);
    Task<List<CountryEntity>> HandleAsync(FindCountryByIdQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindAllReservationsQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationByIdQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByEndDateQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByHotelQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByPriceQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByRoomQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByStartDateQuery query);
    Task<List<ReservationEntity>> HandleAsync(FindReservationsByUserQuery query);
    Task<List<RoomEntity>> HandleAsync(FindAllRoomsQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomByIdQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomsByHotelQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomsByNumberOfPeopleQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomsByPriceQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomsByReservationQuery query);
    Task<List<RoomEntity>> HandleAsync(FindRoomsByTypeQuery query);
    Task<List<RoomTypeEntity>> HandleAsync(FindAllRoomTypesQuery query);
    Task<List<RoomTypeEntity>> HandleAsync(FindRoomTypeByIdQuery query);
    Task<List<RoomReservedEntity>> HandleAsync(FindAllRoomsReservationsQuery query);
    Task<List<RoomReservedEntity>> HandleAsync(FindRoomReservationByIdQuery query);
}
