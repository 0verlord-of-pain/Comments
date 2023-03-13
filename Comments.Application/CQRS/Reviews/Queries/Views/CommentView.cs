namespace Comments.Application.CQRS.Reviews.Queries.Views;

public class CommentView
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public ICollection<CommentView> ChildComment { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}