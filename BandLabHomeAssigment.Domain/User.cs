namespace BandLabHomeAssigment.Domain;

public class User(string name) : DomainEntity
{
    public string Name { get; private set; } = name;
}
