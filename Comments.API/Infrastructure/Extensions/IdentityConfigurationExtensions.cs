using Comments.Domain.Entities;
using Comments.Storage.Persistence;
using Microsoft.AspNetCore.Identity;
namespace Comments.API.Infrastructure.Extensions;

public static class IdentityConfigurationExtensions
{
    public static IServiceCollection AddIdentityCustom(
        this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(ops =>
            {
                ops.User.RequireUniqueEmail = true;
                ops.Password.RequireDigit = false;
                ops.Password.RequireUppercase = false;
                ops.Password.RequireNonAlphanumeric = false;
                ops.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}