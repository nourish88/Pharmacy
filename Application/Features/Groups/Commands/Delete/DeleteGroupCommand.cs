using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Features.Medicines.Commands.Delete;
using Pharmacy.Infrastructure;

namespace Pharmacy.Application.Features.Groups.Commands.Delete;

public sealed record DeletedGroupResponse(int? Id, string? Message = "İşlem Başarılı");
public class DeleteGroupCommand : IRequest<DeletedGroupResponse?>
{
    public int Id { get; set; }
}
public class DeleteGroupCommandHandler(AppDbContext ctx)
    : IRequestHandler<DeleteGroupCommand, DeletedGroupResponse?>
{
    public async Task<DeletedGroupResponse?> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {

        var medicineExist = await ctx.Medicines.AnyAsync(x => x.GroupId == request.Id, cancellationToken: cancellationToken);

        if (medicineExist)
        {
            return new DeletedGroupResponse(0,
                "Grup üzerinde ilaç tanımlı olduğundan grup silinemez. Lütfen öncelikle grupta bulunan ilaçları siliniz.");
        }
        var group = await ctx.Groups.FindAsync(request.Id);
        if (group is null) return null;
        ctx.Groups.Remove(group);
        await ctx.SaveChangesAsync(cancellationToken);

        return new DeletedGroupResponse(request.Id);
    }
}