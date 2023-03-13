using AutoMapper;
using Comments.Application.CQRS.Reviews.Queries.Get;
using Comments.Application.CQRS.Reviews.Queries.Views;
using Comments.Core.Enums;
using Comments.Storage.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comments.Application.CQRS.Reviews.Queries;

public sealed class CommentQueryHandler :
    IRequestHandler<FilterCommentQuery, IEnumerable<CommentView>>
{
    private const int limit = 25;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CommentQueryHandler(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<CommentView>> Handle(
        FilterCommentQuery request,
        CancellationToken cancellationToken)
    {
        var filter = _context.Reviews.Where(i => i.IsBaseReview);
        if (request.SortType.HasValue)
            switch (request.SortType.Value)
            {
                case SortType.UserName:
                    filter = request.IsAscending.HasValue && !request.IsAscending.Value
                        ? filter.OrderByDescending(i => i.User.UserName)
                        : filter.OrderBy(i => i.User.UserName);
                    break;
                case SortType.Email:
                    filter = request.IsAscending.HasValue && !request.IsAscending.Value
                        ? filter.OrderByDescending(i => i.User.Email)
                        : filter.OrderBy(i => i.User.Email);
                    break;
                case SortType.CreatedOnUtc:
                    filter = request.IsAscending.HasValue && !request.IsAscending.Value
                        ? filter.OrderByDescending(i => i.User.CreatedOnUtc)
                        : filter.OrderBy(i => i.User.CreatedOnUtc);
                    break;
            }

        else
            filter = filter.OrderByDescending(i => i.CreatedOnUtc);

        var result = await filter
            .Include(i => i.Reviews)
            .Include(i => i.User)
            .Skip(limit * (request.Page - 1))
            .Take(limit)
            .ToArrayAsync(cancellationToken);

        return _mapper.Map<IEnumerable<CommentView>>(result);
    }
}