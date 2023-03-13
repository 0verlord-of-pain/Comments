using Comments.Application.CQRS.Reviews.Queries.Views;
using Comments.Core.Enums;
using MediatR;

namespace Comments.Application.CQRS.Reviews.Queries.Get;

public class FilterCommentQuery : IRequest<IEnumerable<CommentView>>
{
    public FilterCommentQuery(SortType? sort, bool? isAscending, int page)
    {
        SortType = sort;
        IsAscending = isAscending;
        Page = page;
    }

    public SortType? SortType { get; init; }
    public bool? IsAscending { get; init; }
    public int Page { get; init; }
}