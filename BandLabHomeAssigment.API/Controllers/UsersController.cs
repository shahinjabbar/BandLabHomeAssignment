using BandLabHomeAssigment.API.Dtos.Users;
using BandLabHomeAssigment.Application.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace BandLabHomeAssigment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] ICreateUser createUser, [FromBody] CreateUserDto createUserDto)
    {
        var command = new CreateUserCommand(createUserDto.Name);

        var userId = await createUser.ExecuteAsync(command);
        return CreatedAtAction(nameof(Create), userId);
    }
}
