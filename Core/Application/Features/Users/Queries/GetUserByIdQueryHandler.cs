using Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDTO?>
{
    public string Id { get; }
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
{
    private readonly DataContext _context;
    public GetUserByIdQueryHandler(DataContext context)
    {
        _context = context;
    }
    public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.AppUsers.Where(m => m.Id == request.Id).Select(m => new UserDTO
        {
            Id = m.Id,
            UserName = m.UserName
        }).FirstOrDefaultAsync(cancellationToken);
    }
}
