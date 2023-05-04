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
        Guid tripId,
        int numberOfAdults,
        int numberOfChildrenUpTo3yo,
        int numberOfChildrenUpTo10yo,
        int numberOfChildrenUpTo18yo,
        DateTime startDate,
        int duration,
        string placeOfDeparture,
        float totalPrice
    )
    {
        RaiseEvent(
            new ReservationCreatedEvent
            {
                Id = id,
                User = user,
                TripId = tripId,
                NumberOfAdults = numberOfAdults,
                NumberOfChildrenUpTo10yo = numberOfChildrenUpTo10yo,
                NumberOfChildrenUpTo18yo = numberOfChildrenUpTo18yo,
                NumberOfChildrenUpTo3yo = numberOfChildrenUpTo3yo,
                StartDate = startDate,
                Duration = duration,
                PlaceOfDeparture = placeOfDeparture,
                TotalPrice = totalPrice,
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
        int numberOfAdults,
        int numberOfChildrenUpTo3yo,
        int numberOfChildrenUpTo10yo,
        int numberOfChildrenUpTo18yo,
        DateTime startDate,
        int duration,
        string placeOfDeparture,
        float totalPrice
    )
    {
        if (!Active)
        {
            throw new InvalidOperationException("You cannot edit an inactive reservation.");
        }

        RaiseEvent(
            new ReservationEditedEvent
            {
                Id = Id,
                DateUpdated = DateTime.Now,
                Duration = duration,
                NumberOfAdults = numberOfAdults,
                NumberOfChildrenUpTo10yo = numberOfChildrenUpTo10yo,
                NumberOfChildrenUpTo18yo = numberOfChildrenUpTo18yo,
                NumberOfChildrenUpTo3yo = numberOfChildrenUpTo3yo,
                PlaceOfDeparture = placeOfDeparture,
                StartDate = startDate,
                TotalPrice = totalPrice
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
