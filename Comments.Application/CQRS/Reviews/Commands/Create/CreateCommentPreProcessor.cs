using Comments.Core.Exceptions;
using Comments.Domain.Entities;
using Comments.Storage.Persistence;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Comments.Application.CQRS.Reviews.Commands.Create;

public sealed class CreateCommentPreProcessor : IRequestPreProcessor<CreateCommentCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public CreateCommentPreProcessor(UserManager<User> userManager, DataContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Process(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new NotFoundException("User was not found");

        if (request.ParentReviewId.HasValue)
        {
            var parentReview =
                await _context.Reviews.FirstOrDefaultAsync(i => i.Id == request.ParentReviewId.Value,
                    cancellationToken);
            if (parentReview == null) throw new NotFoundException("Comment was not found");
        }

        if (string.IsNullOrEmpty(request.Text)) throw new ValidationException("Field must not be empty");
    }
}