using AutoMapper;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Application.Features.Customers.Commands.Update;
using Pharmacy.Application.Features.Customers.Queries.GetById;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Features.Customers.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CustomerViewModel>().ReverseMap();   
        CreateMap<Customer, GetByIdCustomerResponse>().ReverseMap();   
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();   
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();   
        
        CreateMap<Customer, CreatedCustomerResponse>().ReverseMap();   
        CreateMap<UpdatedCustomerResponse, UpdateCustomerCommand>().ReverseMap();   
        CreateMap<UpdatedCustomerResponse, Customer>().ReverseMap();   
    }
}