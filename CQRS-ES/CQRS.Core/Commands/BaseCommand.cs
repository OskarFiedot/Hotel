using CQRS.Core.Messages;

namespace CQRS.Core.Commands;

public abstract class BaseCommand : Message
{
    protected BaseCommand(string type)
    {
        this.Type = type;
    }

    public string Type { get; set; }
}
