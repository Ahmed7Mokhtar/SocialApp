using Application.Features.Users.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserDTO>>
{

}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.AppUsers.Select(m => new UserDTO
        {
            Username = m.UserName
        }).ToListAsync(cancellationToken);
    }
}
