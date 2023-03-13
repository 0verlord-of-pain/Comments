using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Comments.Application.CQRS.Users.Queries.GetAllUsers;
using Comments.Application.CQRS.Users.Queries.GetUser;
using Comments.Application.CQRS.Users.Queries.Views;
using Comments.Storage.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Comments.Application.CQRS.Users.Queries;

public sealed class UsersQueryHandler :
    IRequestHandler<GetAllUsersQuery, IEnumerable<UserView>>,
    IRequestHandler<GetUserQuery, UserView>
{
    private const int limit = 25;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UsersQueryHandler(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<UserView>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Page <= 0) throw new ValidationException("Page is too small");

        var users = await _context.Users
            .Skip(limit * (request.Page - 1))
            .Take(limit)
            .ToListAsync(cancellationToken);

        var usersViews = _mapper.Map<IEnumerable<UserView>>(users);

        foreach (var view in usersViews)
        {
            var rolesIds = _context.UserRoles
                .Where(i => i.UserId.Equals(view.Id))
                .Select(i => i.RoleId);

            view.Roles = await _context.Roles
                .Where(i => rolesIds.Contains(i.Id))
                .Select(i => i.Name)
                .ToArrayAsync(cancellationToken);
        }

        return usersViews;
    }

    public async Task<UserView> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(i => i.Id == request.UserId, cancellationToken);

        var view = _mapper.Map<UserView>(user);

        var rolesIds = _context.UserRoles
            .Where(i => i.UserId.Equals(view.Id))
            .Select(i => i.RoleId);

        view.Roles = await _context.Roles
            .Where(i => rolesIds.Contains(i.Id))
            .Select(i => i.Name)
            .ToArrayAsync(cancellationToken);

        return view;
    }
}