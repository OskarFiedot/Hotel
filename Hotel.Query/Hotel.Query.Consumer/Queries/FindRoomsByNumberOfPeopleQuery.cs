using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomsByNumberOfPeopleQuery : BaseQuery
{
    public FindRoomsByNumberOfPeopleQuery()
        : base(nameof(FindRoomsByNumberOfPeopleQuery)) { }

    public int NumberOfPeople { get; set; }
}
