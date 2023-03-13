using Comments.Application.CQRS.Reviews.Queries.Views;
using MediatR;

namespace Comments.Application.CQRS.Reviews.Commands.Create;

public class CreateCommentCommand : IRequest<CommentView>
{
    public CreateCommentCommand(Guid userId, string text, Guid? parentReviewId = null)
    {
        UserId = userId;
        Text = text;
        ParentReviewId = parentReviewId;
    }

    public string Text { get; init; }
    public Guid UserId { get; init; }
    public Guid? ParentReviewId { get; init; }
}