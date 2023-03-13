using Comments.API.Infrastructure.Helpers;
using Comments.API.Persistence;
using Comments.Application.CQRS.Reviews.Commands.Create;
using Comments.Application.CQRS.Reviews.Commands.Delete;
using Comments.Application.CQRS.Reviews.Queries.Get;
using Comments.Application.CQRS.Reviews.Queries.Views;
using Comments.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comments.API.Controllers;

public class CommentController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ICollection<CommentView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] SortType? sortType = null,
        [FromQuery] bool? isAscending = true,
        [FromQuery] int? page = 1)
    {
        var query = new FilterCommentQuery(sortType, isAscending, page.Value);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(Policy = Policies.User)]
    [ProducesResponseType(typeof(ICollection<CommentView>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromQuery] Guid? commentId,
        [FromQuery] string text)
    {
        text = XCCSanitizeHelper.SanitizeText(text);
        var result = await _mediator.Send(new CreateCommentCommand(UserId, text, commentId));
        return Created("", result);
    }

    [HttpDelete("{reviewId}")]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid reviewId)
    {
        await _mediator.Send(new DeleteCommentCommand(reviewId, UserId));
        return NoContent();
    }
}