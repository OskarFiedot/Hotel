namespace CQRS.Core.Events;
using CQRS.Core.Messages;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        this.Type = type;
    }
    public int Version { get; set; }
    public string Type { get; set; }
}