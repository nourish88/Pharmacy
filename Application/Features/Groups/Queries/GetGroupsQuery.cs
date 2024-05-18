using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Groups.Queries;

public class GetGroupsQuery : IRequest<GetGroupsResponse>
{
    public int? Skip { get; set; } = 0;
    public string? Name { get; set; }

    public int? Take { get; set; } = 5;

}
public class GetGroupsQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetGroupsQuery, GetGroupsResponse>
{
    public async Task<GetGroupsResponse> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {


        var groupRepo = dbContext.Groups.AsQueryable();
        if (request.Name != null && !string.IsNullOrWhiteSpace(request.Name) && request.Name != "null")
        {
            groupRepo = groupRepo.Where(predicate: b => b.Name.Contains(request.Name));
        }

        var groups = new List<GroupViewModel>();
        if (request.Skip != null)
        {
             groups = await groupRepo.Select(q => new GroupViewModel(q.Id, q.Name
            )).Skip(request.Skip.Value * request.Take.Value).Take(request.Take.Value).ToListAsync(cancellationToken: cancellationToken);
             var model = new GetGroupsResponse { Groups = groups, TotalRecords = groupRepo.Count() };
             return model;
        }
        
            groups = await groupRepo.Select(q => new GroupViewModel(q.Id, q.Name
            )).ToListAsync(cancellationToken: cancellationToken);

            return new GetGroupsResponse { Groups = groups, TotalRecords = groupRepo.Count() };
    }
}
public class GetAllGroupsQuery : IRequest<GetAllGroupsResponse>
{
 

}
public class GetAllGroupsQueryHandler(IMapper mapper, AppDbContext dbContext) : IRequestHandler<GetAllGroupsQuery, GetAllGroupsResponse>
{
    public  async Task<GetAllGroupsResponse> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groupRepo = dbContext.Groups.AsQueryable();
        var groups = await groupRepo.Select(q => new AllGroupViewModel(q.Id, q.Name
        )).ToListAsync(cancellationToken: cancellationToken);

        return new GetAllGroupsResponse { Groups = groups };
    }
}