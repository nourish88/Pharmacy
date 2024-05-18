using AutoMapper;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Application.Features.Customers.Commands.Update;
using Pharmacy.Application.Features.Customers.Queries.GetById;
using Pharmacy.Application.Features.Medicines.Commands.Create;
using Pharmacy.Application.Features.Medicines.Commands.Update;
using Pharmacy.Application.Features.Medicines.Queries.GetById;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Features.Medicines.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Medicine, MedicineViewModel>().ForMember(
            dest => dest.GroupName,
            opt => opt.MapFrom(src => src.Group.Name)
        ).ReverseMap();
        CreateMap<Medicine, BaseMedicineViewModel>().ReverseMap();
        CreateMap<Medicine, GetByIdMedicineResponse>().ReverseMap();
        CreateMap<Medicine, CreateMedicineCommand>().ReverseMap();
        CreateMap<Medicine, UpdateMedicineCommand>().ReverseMap();
        CreateMap<Medicine, CreatedMedicineResponse>().ReverseMap();
        CreateMap<UpdatedMedicineResponse, UpdateMedicineCommand>().ReverseMap();
        CreateMap<UpdatedMedicineResponse, Medicine>().ReverseMap();
    }
}