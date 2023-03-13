using Comments.Core.Exceptions;
using Comments.Domain.Entities;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Comments.Application.CQRS.Roles.Commands.Attach;

public sealed class AttachRolePreProcessor : IRequestPreProcessor<AttachRoleCommand>
{
    private readonly UserManager<User> _userManager;

    public AttachRolePreProcessor(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Process(AttachRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new NotFoundException("User was not found");

        var userPolicy = await _userManager.GetRolesAsync(user);
        if (userPolicy.Contains(request.Role.ToString()))
            throw new ValidationException($"User already has {request.Role} role");
    }
}