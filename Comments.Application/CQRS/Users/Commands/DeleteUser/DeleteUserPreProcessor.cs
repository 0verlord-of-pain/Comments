using Comments.Core.Exceptions;
using Comments.Storage.Persistence;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Comments.Application.CQRS.Users.Commands.DeleteUser;

public sealed class DeleteUserPreProcessor : IRequestPreProcessor<DeleteUserCommand>
{
    private readonly DataContext _context;

    public DeleteUserPreProcessor(DataContext context)
    {
        _context = context;
    }

    public async Task Process(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == request.UserId, cancellationToken);

        if (user == null) throw new NotFoundException("User was not found");
    }
}