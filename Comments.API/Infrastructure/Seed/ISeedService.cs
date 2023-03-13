namespace Comments.API.Infrastructure.Seed;
internal interface ISeedService
{
    Task SeedRolesAsync();
    Task SeedAdminAndManagerAsync();
}