using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Services.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService authorService;
    private readonly IValidator<AuthorRequest> authorValidator;

    public AuthorController(IAuthorService authorService, IValidator<AuthorRequest> authorValidator)
    {
        this.authorService = authorService;
        this.authorValidator = authorValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll (CancellationToken ct)
    {
        var authors = await  authorService.GetAllAsync(ct);
        if(authors.Count == 0)
        {
            return Ok("No authors found");
        }
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById ([FromRoute] long id, CancellationToken ct)
    {
        var author = await authorService.GetByIdAsync(id,ct);
        if(author is null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost()]
    public async Task<IActionResult> Insert ([FromBody] AuthorRequest request, CancellationToken ct)
    {
        var validationResult = await authorValidator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var author = await authorService.AddAsync(request, ct);
        if(author is null)
        {
            return BadRequest();
        }
        return Ok(author);
    }

    [HttpPut]
    public async Task<IActionResult> Update ([FromBody] UpdateAuthorRequest request, CancellationToken ct)
    {
        var success = await authorService.UpdateAsync(request, ct);
        if (!success)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete ([FromQuery] long id, CancellationToken ct)
    {
        var success = await authorService.RemoveAsync(id, ct);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}