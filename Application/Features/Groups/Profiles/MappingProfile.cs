using AutoMapper;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Groups.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Features.Groups.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, GroupViewModel>().ReverseMap();
        }
    }
}
