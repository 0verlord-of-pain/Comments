using Comments.API.Infrastructure.Seed;

public struct SeedData
{
    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
        await seedService.SeedRolesAsync();
        await seedService.SeedAdminAndManagerAsync();
    }
}