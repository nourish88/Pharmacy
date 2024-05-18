using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Abstractions.Dtos;
using Pharmacy.Application.Features.Customers.Queries.GetByName;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Customers.Queries.Get;

public class GetCustomersQuery : IRequest<GetByNameCustomersResponse>
{
    public string? Name { get; set; }
    public string? SurName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
   public int? Id { get; set; }
   public int Skip { get; set; } = 0;

   public int Take { get; set; } = 10;

}

public class GetByNameCustomerQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetCustomersQuery, GetByNameCustomersResponse>
{
    public async Task<GetByNameCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerRepo = dbContext.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            customerRepo = customerRepo.Where(x => x.Name != null && x.Name.Contains(request.Name));
        }
        if (!string.IsNullOrWhiteSpace(request.SurName))
        {
            customerRepo = customerRepo.Where(x => x.SurName != null && x.SurName.Contains(request.SurName));
        }
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            customerRepo = customerRepo.Where(x => x.Email != null && x.Email.Contains(request.Email));
        }
        if (!string.IsNullOrWhiteSpace(request.Address))
        {
            customerRepo = customerRepo.Where(x => x.Address != null && x.Address.Contains(request.Address));
        }
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            customerRepo = customerRepo.Where(x => x.PhoneNumber != null && x.PhoneNumber.Contains(request.PhoneNumber));
        }
        if (request.Id!=null)
        {
            customerRepo = customerRepo.Where(x => x.Id ==Convert.ToInt32(request.Id) );
        }

      

        try
        {
            var customers = await customerRepo.Skip(request.Skip * request.Take).Take(request.Take).ToListAsync(cancellationToken: cancellationToken);

            var response = mapper.Map<List<CustomerViewModel>>(customers);
            var model = new GetByNameCustomersResponse
            {
                Customers = response,
                CurrentPage = 1,
                TotalRecords = await customerRepo.CountAsync()
            };
            return model;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}