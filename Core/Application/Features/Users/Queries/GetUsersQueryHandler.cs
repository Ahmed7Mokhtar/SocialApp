using Application.Features.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Extenstions;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users.Queries;

public class GetUsersQuery : IRequest<IEnumerable<MemberDTO>>
{

}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<MemberDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly DataContext _context;

    public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository, DataContext context)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _context = context;
    }
    public async Task<IEnumerable<MemberDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        //return await _context.AppUsers
        //    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
        //    .ToListAsync(cancellationToken);

        return await _context.AppUsers
            .Select(m => new MemberDTO
            {
                Id = m.Id,
                Age = m.DateOfBirth.CalcAge(),
                Created = m.Created,
                Interests = m.Interests,
                Introduction = m.Introduction,
                Gender = m.Gender,
                KnownAs = m.KnownAs,
                LookingFor = m.LookingFor,
                LastActive = m.LastActive,
                Username = m.UserName,
                Address = m.Address != null ? new AddressDTO { City = m.Address.City, Country = m.Address.Country, Id = m.Address.Id } : null,
                Photos = m.Photos.Select(p => new PhotoDTO
                {
                    Id = p.Id,
                    IsMain = p.IsMain,
                    Url = p.Url
                }).ToList()
            }).ToListAsync(cancellationToken);
    }
}
