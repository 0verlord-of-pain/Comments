namespace Comments.Domain.Entities;

public class Review : IBaseEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public Guid? ParentReviewId { get; set; }
    public Review ParentReview { get; set; }
    public bool IsBaseReview { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }

    public static Review CreateBase(string text, User user)
    {
        return new Review
        {
            Text = text,
            User = user,
            UserId = user.Id,
            IsBaseReview = true
        };
    }

    public static Review CreateChild(string text, User user, Review parentReview)
    {
        return new Review
        {
            Text = text,
            User = user,
            UserId = user.Id,
            ParentReview = parentReview,
            ParentReviewId = parentReview.Id,
            IsBaseReview = false
        };
    }

    public void SoftDelete()
    {
        IsArchived = false;
    }

    public void Restore()
    {
        IsArchived = true;
    }
}