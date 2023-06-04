namespace CQRS.Core.Queries;

public abstract class BaseQuery
{
    protected BaseQuery(string type)
    {
        this.Type = type;
    }

    public string Type { get; set; }
}
