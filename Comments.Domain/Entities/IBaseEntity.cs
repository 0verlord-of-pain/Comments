using Comments.Domain.Extensions;
namespace Comments.Domain.Entities;

public interface IBaseEntity : ICreatedOnUtc, IUpdatedOnUtc
{
    public Guid Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? UpdatedOnUtc { get; set; }
    public bool IsArchived { get; set; }
    public void SoftDelete(){}
    public void Restore(){}
}