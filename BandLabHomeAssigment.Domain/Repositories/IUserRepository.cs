namespace BandLabHomeAssigment.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid> AddAsync(User user);
}
