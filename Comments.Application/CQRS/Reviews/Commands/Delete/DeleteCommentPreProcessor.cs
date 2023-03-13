using Comments.Core.Exceptions;
using Comments.Domain.Entities;
using Comments.Storage.Persistence;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Comments.Application.CQRS.Reviews.Commands.Delete;

public sealed class DeleteCommentPreProcessor : IRequestPreProcessor<DeleteCommentCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public DeleteCommentPreProcessor(UserManager<User> userManager, DataContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Process(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserWhoDeleteId.ToString());
        if (user == null) throw new NotFoundException("User was not found");

        var review = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == request.ReviewId, cancellationToken);
        if (review == null) throw new NotFoundException("Comment was not found");

        if (review.UserId != request.UserWhoDeleteId)
        {
            var userPolicy = await _userManager.GetRolesAsync(user);
            if (!userPolicy.Contains(Core.Enums.Roles.Admin.ToString())
                && !userPolicy.Contains(Core.Enums.Roles.Manager.ToString()))
                throw new ForbidException("You do not have permission to do this");
        }
    }
}