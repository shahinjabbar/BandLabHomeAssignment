using BandLabHomeAssigment.Domain.Repositories;
using BandLabHomeAssigment.Domain;
namespace BandLabHomeAssigment.Application.Commands.CreateUser;

public class CreateUser(IUserRepository userRepository) : ICreateUser
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> ExecuteAsync(CreateUserCommand command)
    {
        var userId = await _userRepository.AddAsync(new User(command.Name));
        return userId;
    }
}
