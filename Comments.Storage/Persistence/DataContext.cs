using Comments.Domain.Entities;
using Comments.Storage.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Comments.Storage.Persistence;

public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DataContext(
        DbContextOptions<DataContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        if (Database.EnsureCreated()) Database.Migrate();
    }

    public DbSet<Review> Reviews { get; set; }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.UpdateSystemDates();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}