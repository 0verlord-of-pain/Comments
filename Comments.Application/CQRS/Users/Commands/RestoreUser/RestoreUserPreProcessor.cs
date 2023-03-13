using Comments.Core.Exceptions;
using Comments.Storage.Persistence;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Comments.Application.CQRS.Users.Commands.RestoreUser;

public sealed class RestoreUserPreProcessor : IRequestPreProcessor<RestoreUserCommand>
{
    private readonly DataContext _context;

    public RestoreUserPreProcessor(DataContext context)
    {
        _context = context;
    }

    public async Task Process(RestoreUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(i => i.Id == request.UserId, cancellationToken);
        if (user == null) throw new NotFoundException("User was not found");
    }
}