using Comments.Application.CQRS.Users.Queries.Views;
using MediatR;

namespace Comments.Application.CQRS.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserView>>
{
    public GetAllUsersQuery(int page)
    {
        Page = page;
    }

    public int Page { get; init; }
}