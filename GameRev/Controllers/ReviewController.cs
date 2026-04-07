using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Models.Entities;
using GameRev.Services.Entities.Interfaces;
using GameRev.Validators;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    
    private readonly IReviewService reviewService;
    private readonly ReviewValidators reviewValidator;

    public ReviewController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var reviews =  await reviewService.GetAllAsync(ct);
        if (reviews.Count == 0)
        {
            return Ok("No reviews found");
        }
        return Ok(reviews);
    }

    [HttpGet("/videogame/{videogameId}")]
    public async Task<IActionResult> GetReviewsForVideogame([FromRoute] long videogameId, CancellationToken ct)
    {
        //validators
        var reviews = await reviewService.GetGameReviewsAsync(videogameId,ct);
        if(reviews.Count == 0)
        {
            return Ok("No reviews found for this game");
        }
        return Ok(reviews);
    }

    [HttpGet("/user/{userId}")]
    public async Task<IActionResult> GetUserReviews([FromRoute] long userId, CancellationToken ct)
    {
        //validator
        var reviews = await reviewService.GetUserReviewsAsync(userId,ct);
        if(reviews.Count == 0)
        {
            return Ok("No reviews found for this user");
        }
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewReview (ReviewRequest request, CancellationToken ct)
    {
        var validationResult = await reviewValidator.ValidateAsync(request,ct);
        if (!validationResult.IsValid)
        {
            return BadRequest("Validation Error");
        }
        var review = reviewService.AddAsync(request,ct);
        if(review is null)
        {
            return BadRequest();
        }
        return Ok(review);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateReview (UpdateReviewRequest request, CancellationToken ct)
    {
        //validator
        var success = await reviewService.UpdateAsync(request,ct);
        if (!success)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReview (long id, CancellationToken ct)
    {
        var success = await reviewService.DeleteAsync(id,ct);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

}