using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;

namespace Hotel.Query.Consumer.Queries;

public class QueryHandler : IQueryHandler
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IRoomReservedRepository _roomReservedRepository;

    public QueryHandler(
        IReservationRepository reservationRepository,
        ICityRepository cityRepository,
        ICountryRepository countryRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IRoomTypeRepository roomTypeRepository,
        IRoomReservedRepository roomReservedRepository
    )
    {
        _reservationRepository = reservationRepository;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _roomTypeRepository = roomTypeRepository;
        _roomReservedRepository = roomReservedRepository;
    }

    public async Task<List<HotelEntity>> HandleAsync(FindAllHotelsQuery query)
    {
        return await _hotelRepository.ListAllAsync();
    }

    public async Task<List<HotelEntity>> HandleAsync(FindHotelByIdQuery query)
    {
        var hotel = await _hotelRepository.GetByIdAsync(query.Id);
        return new List<HotelEntity> { hotel };
    }

    public async Task<List<HotelEntity>> HandleAsync(FindHotelsByCityQuery query)
    {
        return await _hotelRepository.ListByCityAsync(query.CityName);
    }

    public async Task<List<HotelEntity>> HandleAsync(FindHotelsByCountryQuery query)
    {
        return await _hotelRepository.ListByCountryAsync(query.CountryName);
    }

    public async Task<List<CityEntity>> HandleAsync(FindAllCitiesQuery query)
    {
        return await _cityRepository.ListAllAsync();
    }

    public async Task<List<CityEntity>> HandleAsync(FindCityByIdQuery query)
    {
        var city = await _cityRepository.GetByIdAsync(query.Id);
        return new List<CityEntity> { city };
    }

    public async Task<List<CityEntity>> HandleAsync(FindCitiesByCountryQuery query)
    {
        return await _cityRepository.ListByCountryAsync(query.CountryName);
    }

    public async Task<List<CountryEntity>> HandleAsync(FindAllCountriesQuery query)
    {
        return await _countryRepository.ListAllAsync();
    }

    public async Task<List<CountryEntity>> HandleAsync(FindCountryByIdQuery query)
    {
        var country = await _countryRepository.GetByIdAsync(query.Id);
        return new List<CountryEntity> { country };
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindAllReservationsQuery query)
    {
        return await _reservationRepository.ListAllAsync();
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationByIdQuery query)
    {
        var reservation = await _reservationRepository.GetByIdAsync(query.Id);
        return new List<ReservationEntity> { reservation };
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByEndDateQuery query)
    {
        return await _reservationRepository.ListByEndDateAsync(
            DateOnly.FromDateTime(query.EndDate)
        );
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByHotelQuery query)
    {
        return await _reservationRepository.ListByHotelAsync(query.HotelId);
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByPriceQuery query)
    {
        return await _reservationRepository.ListByPriceAsync(query.MinPrice, query.MaxPrice);
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByRoomQuery query)
    {
        return await _reservationRepository.ListByRoomAsync(query.RoomId);
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByStartDateQuery query)
    {
        return await _reservationRepository.ListByStartDateAsync(
            DateOnly.FromDateTime(query.StartDate)
        );
    }

    public async Task<List<ReservationEntity>> HandleAsync(FindReservationsByUserQuery query)
    {
        return await _reservationRepository.ListByUserAsync(query.User);
    }

    public async Task<List<RoomEntity>> HandleAsync(FindAllRoomsQuery query)
    {
        return await _roomRepository.ListAllAsync();
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomByIdQuery query)
    {
        var room = await _roomRepository.GetByIdAsync(query.Id);
        return new List<RoomEntity> { room };
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomsByHotelQuery query)
    {
        return await _roomRepository.ListByHotelAsync(query.HotelId);
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomsByNumberOfPeopleQuery query)
    {
        return await _roomRepository.ListByNumberOfPeopleAsync(query.NumberOfPeople);
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomsByPriceQuery query)
    {
        return await _roomRepository.ListByPriceAsync(query.MinPrice, query.MaxPrice);
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomsByReservationQuery query)
    {
        return await _roomRepository.ListByReservationAsync(query.ReservationId);
    }

    public async Task<List<RoomEntity>> HandleAsync(FindRoomsByTypeQuery query)
    {
        return await _roomRepository.ListByRoomTypeAsync(query.RoomTypeId);
    }

    public async Task<List<RoomTypeEntity>> HandleAsync(FindAllRoomTypesQuery query)
    {
        return await _roomTypeRepository.ListAllAsync();
    }

    public async Task<List<RoomTypeEntity>> HandleAsync(FindRoomTypeByIdQuery query)
    {
        var roomType = await _roomTypeRepository.GetByIdAsync(query.Id);
        return new List<RoomTypeEntity> { roomType };
    }

    public async Task<List<RoomReservedEntity>> HandleAsync(FindAllRoomsReservationsQuery query)
    {
        return await _roomReservedRepository.ListAllAsync();
    }

    public async Task<List<RoomReservedEntity>> HandleAsync(FindRoomReservationByIdQuery query)
    {
        var roomReservation = await _roomReservedRepository.GetByIdAsync(query.Id);
        return new List<RoomReservedEntity> { roomReservation };
    }
}
