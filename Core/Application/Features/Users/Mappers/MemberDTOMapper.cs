using Application.Features.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Extenstions;

namespace Application.Features.Users.Mappers
{
    public class MemberDTOMapper : Profile
    {
        public MemberDTOMapper()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Created))
                .ForMember(dest => dest.Interests, opts => opts.MapFrom(src => src.Interests))
                .ForMember(dest => dest.Introduction, dest => dest.MapFrom(src => src.Introduction))
                .ForMember(dest => dest.KnownAs, opts => opts.MapFrom(src => src.KnownAs))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.LookingFor, opts => opts.MapFrom(src => src.LookingFor))
                .ForMember(dest => dest.Age, opts => opts.MapFrom(src => src.DateOfBirth.CalcAge()));

            CreateMap<Photo, PhotoDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsMain, opts => opts.MapFrom(src => src.IsMain))
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url));

            CreateMap<UserAddress, AddressDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country));
        }
    }
}
