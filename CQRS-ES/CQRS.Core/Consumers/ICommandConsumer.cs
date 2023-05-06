namespace CQRS.Core.Consumers;

public interface ICommandConsumer
{
    void Consume(string queue);
}
