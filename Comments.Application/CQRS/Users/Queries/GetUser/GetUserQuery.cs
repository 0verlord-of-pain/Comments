using Comments.Application.CQRS.Users.Queries.Views;
using MediatR;

namespace Comments.Application.CQRS.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserView>
{
    public Guid UserId { get; init; }

    public GetUserQuery Create(Guid userId)
    {
        return new GetUserQuery
        {
            UserId = userId
        };
    }
}