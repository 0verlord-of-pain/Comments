using MediatR;

namespace Comments.Application.CQRS.Users.Commands.RestoreUser;

public class RestoreUserCommand : IRequest<Guid>
{
    public RestoreUserCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }
}