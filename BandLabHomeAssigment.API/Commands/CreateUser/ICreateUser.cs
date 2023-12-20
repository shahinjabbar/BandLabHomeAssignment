namespace BandLabHomeAssigment.Application.Commands.CreateUser;

public interface ICreateUser
{
    Task<Guid> ExecuteAsync(CreateUserCommand command);
}
