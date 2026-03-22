using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Services.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase{
    
    private readonly IUserService userService;

    public UserController (IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll (CancellationToken ct)
    {
        var users = await userService.GetAllAsync(ct);
        if(users.Count == 0)
        {
            return Ok("No users found");
        }
        return Ok(users);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetById ([FromRoute] long id, CancellationToken ct)
    {
        var user = await userService.GetByIdAsync(id, ct);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }


    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail ([FromRoute] string email, CancellationToken ct)
    {
        var user = await userService.GetByEmailAsync(email,ct);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetByUsername ([FromRoute] string username, CancellationToken ct)
    {
        var user = await userService.GetByUsernameAsync(username,ct);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Insert (UserRequest request, CancellationToken ct)
    {
        //validator
        var user = await userService.AddAsync(request, ct);
        if(user is null)
        {
            return BadRequest();
        }
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update (UpdateUserRequest request, CancellationToken ct)
    {
        var success = await userService.UpdateAsync(request, ct);
        if(!success)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete ([FromRoute] long id, CancellationToken ct)
    {
        var success = await userService.RemoveAsync(id, ct);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

}