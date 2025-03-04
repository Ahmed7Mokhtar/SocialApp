using Application.Features.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Extenstions;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<MemberDTO?>
{
    public string Id { get; }
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, MemberDTO?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper, DataContext context)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _context = context;
    }
    public async Task<MemberDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        //return await _context.AppUsers
        //    .Where(m => m.Id == request.Id)
        //    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
        //    .FirstOrDefaultAsync(cancellationToken);

        return await _context.AppUsers
            .Where(m => m.Id == request.Id)
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
            }).FirstOrDefaultAsync(cancellationToken);
    }
}
