using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Groups.Commands.Create;

public class CreateGroupCommand : IRequest<CreatedGroupResponse>
{
    public required string Name { get; set; }
}

public class CreateGroupCommandHandler(AppDbContext ctx) : IRequestHandler<CreateGroupCommand,CreatedGroupResponse>
{
    public async  Task<CreatedGroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var repo = ctx.Groups;
        if (await repo.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken))
        {
            return new CreatedGroupResponse(string.Empty, 0, "Aynı isimle kayıt eklenmiştir.");
        }

        var data = new Group
        {
            Name = request.Name
        };
        await repo.AddAsync(data, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);

        return new CreatedGroupResponse(data.Name, data.Id);
    }
}