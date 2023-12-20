namespace BandLabHomeAssigment.Domain;

public class DomainEntity
{
    public Guid Id { get; }

    protected DomainEntity()
    {
        Id = Guid.NewGuid();
    }
}
