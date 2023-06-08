namespace CQRS.Core.Consumers;

public interface IQueryConsumer
{
    void Consume(string queue);
}
