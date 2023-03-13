namespace Comments.Application.CQRS.Users.Queries.Views;

public class UserView
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
    public bool IsArchived { get; set; }
    public IEnumerable<string> Roles { get; set; }
}