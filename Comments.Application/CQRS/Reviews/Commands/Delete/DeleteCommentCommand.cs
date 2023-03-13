using MediatR;

namespace Comments.Application.CQRS.Reviews.Commands.Delete;

public class DeleteCommentCommand : IRequest<Unit>
{
    public DeleteCommentCommand(Guid reviewId, Guid userWhoDeleteId)
    {
        ReviewId = reviewId;
        UserWhoDeleteId = userWhoDeleteId;
    }

    public Guid ReviewId { get; init; }
    public Guid UserWhoDeleteId { get; init; }
}
