using CQRS.Core.Domain;
using Hotel.Common.Events;

namespace Hotel.Cmd.Domain.Aggregates;

public class ReservationAggregate : AggregateRoot
{
    public bool Active { get; set; }

    private string _user;

    public ReservationAggregate() { }

    public ReservationAggregate(
        Guid id,
        string user,
        DateTime startDate,
        DateTime endDate,
        float totalPrice,
        Guid hotel_id,
        List<Guid> roomReserved
    )
    {
        RaiseEvent(
            new ReservationCreatedEvent
            {
                Id = id,
                User = user,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice,
                Hotel = hotel_id,
                RoomReserved = roomReserved,
                DateReserved = DateTime.Now
            }
        );
    }

    public void Apply(ReservationCreatedEvent @event)
    {
        Id = @event.Id;
        Active = true;
        _user = @event.User;
    }

    public void EditReservation(
        DateTime startDate,
        DateTime endDate,
        float totalPrice,
        List<Guid> roomReserved
    )
    {
        if (!Active)
        {
            throw new InvalidOperationException("You cannot edit an inactive reservation.");
        }

        RaiseEvent(
            new ReservationEditedEvent
            {
                Id = this.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice,
                RoomReserved = roomReserved,
                DateEdited = DateTime.Now
            }
        );
    }

    public void Apply(ReservationEditedEvent @event)
    {
        Id = @event.Id;
    }

    public void CancelReservation(string username)
    {
        if (!Active)
        {
            throw new InvalidOperationException("The reservation has already been cancelled.");
        }

        if (!_user.Equals(username, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException(
                "You are not allowed to cancell a reservation that was made by someone else."
            );
        }

        RaiseEvent(new ReservationCancelledEvent { Id = this.Id });
    }

    public void Apply(ReservationCancelledEvent @event)
    {
        Id = @event.Id;
        Active = false;
    }
}
