using BandLabHomeAssigment.Domain;
using BandLabHomeAssigment.Domain.Repositories;

namespace BandLabHomeAssigment.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }
}
