using AutoMapper;
using Comments.Application.CQRS.Reviews.Commands.Create;
using Comments.Application.CQRS.Reviews.Commands.Delete;
using Comments.Application.CQRS.Reviews.Queries.Views;
using Comments.Domain.Entities;
using Comments.Storage.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Comments.Application.CQRS.Reviews.Commands;

public class CommentCommandHandler :
    IRequestHandler<CreateCommentCommand, CommentView>,
    IRequestHandler<DeleteCommentCommand, Unit>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CommentCommandHandler(UserManager<User> userManager, DataContext context, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }

    public async Task<CommentView> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        var newReview = new Review();

        if (request.ParentReviewId.HasValue)
        {
            var parentReview = await _context.Reviews
                .FirstOrDefaultAsync(i => i.Id == request.ParentReviewId.Value, cancellationToken);
            newReview = Review.CreateChild(request.Text, user, parentReview);
            await _context.Reviews.AddAsync(newReview, cancellationToken);
        }

        else
        {
            newReview = Review.CreateBase(request.Text, user);
            await _context.Reviews.AddAsync(newReview, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<CommentView>(newReview);

        return result;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(i => i.Id == request.ReviewId, cancellationToken);

        if (review.IsBaseReview)
        {
            foreach (var childReview in review.Reviews)
            {
                childReview.ParentReviewId = null;
                childReview.IsBaseReview = true;
            }

            review.SoftDelete();
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}