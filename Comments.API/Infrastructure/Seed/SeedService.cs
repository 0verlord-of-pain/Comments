using Comments.Core.Enums;
using Comments.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace Comments.API.Infrastructure.Seed;
internal sealed class SeedService : ISeedService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<User> _userManager;

    public SeedService(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRolesAsync()
    {
        await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin.ToString()));
        await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Manager.ToString()));
        await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User.ToString()));
    }

    public async Task SeedAdminAndManagerAsync()
    {
        var defaultAdmin = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (_userManager.Users.All(u => u.Id != defaultAdmin.Id))
        {
            var user = await _userManager.FindByEmailAsync(defaultAdmin.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(defaultAdmin, "admin1");
                await _userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                await _userManager.AddToRoleAsync(defaultAdmin, Roles.Manager.ToString());
                await _userManager.AddToRoleAsync(defaultAdmin, Roles.User.ToString());
            }
        }

        var defaultManager = new User
        {
            UserName = "manager",
            Email = "manager@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (_userManager.Users.All(u => u.Id != defaultManager.Id))
        {
            var user = await _userManager.FindByEmailAsync(defaultManager.Email);
            if (user == null)
            {
                await _userManager.CreateAsync(defaultManager, "manager1");
                await _userManager.AddToRoleAsync(defaultManager, Roles.Manager.ToString());
                await _userManager.AddToRoleAsync(defaultManager, Roles.User.ToString());
            }
        }
    }
}
