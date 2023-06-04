namespace Domain.Entities;

public abstract class BaseDomain
{
    public Guid Id { get; set; }

    protected BaseDomain(Guid id)
    {
        Id = id;
    }
}