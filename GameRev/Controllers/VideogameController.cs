using GameRev.DTOs.Filters;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Services.Entities.Interfaces;
using GameRev.Validators;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api/videogame")]
public class VideogameController : ControllerBase
{

    private readonly IVideogameService videogameService;
    private readonly VideogameRequestValidators videogameValidator;

    public VideogameController(IVideogameService videogameService)
    {
        this.videogameService = videogameService;
    }

    [HttpGet("casual/{limit}")]
    public async Task<IActionResult> GetCasualGames ([FromRoute] int limit, CancellationToken ct)
    {
        var games = await videogameService.GetCasualGames(limit,ct);
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById ([FromRoute] long id, CancellationToken ct)
    {
        var videogame = videogameService.GetByIdAsync(id,ct);
        if(videogame is null)
        {
            return NotFound("The specified videogame can't be found in the system");
        }
        return Ok(videogame);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search (
    [FromQuery] VideogameSearchFilter filter, 
    [FromQuery] int elementsToShow, 
    [FromQuery] int page,
    CancellationToken ct)
    {
        var videogames = await videogameService.SearchAsync(filter, page, elementsToShow, ct);
        if (!videogames.Elements.Any())
        {
            return Ok("No games found for the specified filters");
        }
        return Ok(videogames);
    }

    [HttpGet("most-liked")]
    public async Task<IActionResult> GetMostLiked ([FromQuery] int page, [FromQuery] int elementsToShow, CancellationToken ct)
    {
        var videogames = await videogameService.GetMostLikedAsync(page,elementsToShow,ct);
        if (!videogames.Elements.Any())
        {
            return Ok("No games found");
        }
        return Ok(videogames);
    }

    [HttpGet("new")]
    public async Task<IActionResult> GetNew ([FromQuery] int page, [FromQuery] int elementsToShow, CancellationToken ct)
    {
        var videogames = await videogameService.GetNewAsync(page,elementsToShow,ct);
        if (!videogames.Elements.Any())
        {
            return Ok("No games found");
        }
        return Ok(videogames);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewVideogame ([FromBody] VideogameRequest request, CancellationToken ct)
    {
        var validationResult = await videogameValidator.ValidateAsync(request,ct);
        if (!validationResult.IsValid)
        {
            return BadRequest("Validation Error");
        }
        var videogame = await videogameService.AddAsync(request,ct);
        if(videogame is null)
        {
            return BadRequest();
        }
        return Ok(videogame);
    }

    [HttpPut]
    public async Task<IActionResult> EditVideogame ([FromBody] UpdateVideogameRequest request, CancellationToken ct)
    {
        //validator
        var success = await videogameService.UpdateAsync(request, ct);
        if(!success)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideogame ([FromRoute] long id, CancellationToken ct)
    {
        var success = await videogameService.RemoveAsync(id,ct);
        if (!success)
        {
            return BadRequest();
        }
        return NoContent();
    }
}