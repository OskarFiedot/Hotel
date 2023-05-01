using CQRS.Core.Events;

namespace CQRS.Core.Domain;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    public List<BaseEvent> Changes { get; } = new();
    public int Version { get; set; } = -1;

    public void MarkChangesAsCommited()
    {
        Changes.Clear();
    }

    private void ApplyChanges(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

        if (method == null)
        {
            throw new ArgumentNullException(
                nameof(method),
                $"The Apply method was not found in the aggregate for {@event.GetType().Name}"
            );
        }

        method.Invoke(this, new object[] { @event });

        if (isNew)
        {
            Changes.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChanges(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChanges(@event, false);
        }
    }
}
