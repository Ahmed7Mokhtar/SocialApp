using Application.Features.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Extenstions;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<MemberDTO?>
    {
        public string Username { get; set; }
        public GetUserByUsernameQuery(string username)
        {
            this.Username = username;
        }
    }
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, MemberDTO?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public GetUserByUsernameQueryHandler(IUserRepository userRepository, IMapper mapper, DataContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDTO?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            //return await _context.AppUsers
            //    .Where(m => m.UserName == request.Username)
            //    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(cancellationToken);

            return await _context.AppUsers
                .Where(m => m.UserName == request.Username)
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
}
