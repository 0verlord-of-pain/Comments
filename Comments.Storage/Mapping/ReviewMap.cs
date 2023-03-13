using Comments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comments.Storage.Mapping;

public sealed class ReviewMap : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");
        builder.HasKey(item => item.Id);
        builder.HasQueryFilter(i => !i.IsArchived);

        builder
            .HasIndex(item => item.Id)
            .IsUnique();

        builder
            .HasOne(item => item.User)
            .WithMany(item => item.Reviews)
            .HasForeignKey(item => item.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(item => item.ParentReview)
            .WithMany(item => item.Reviews)
            .HasForeignKey(item => item.ParentReviewId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}