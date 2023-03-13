using Comments.Application.CQRS.Users.Queries.Views;
using MediatR;

namespace Comments.Application.CQRS.Roles.Commands.Remove;

public class RemoveRoleCommand : IRequest<UserView>
{
    public RemoveRoleCommand(Guid userId, Core.Enums.Roles role)
    {
        Role = role;
        UserId = userId;
    }

    public Core.Enums.Roles Role { get; init; }
    public Guid UserId { get; init; }
}