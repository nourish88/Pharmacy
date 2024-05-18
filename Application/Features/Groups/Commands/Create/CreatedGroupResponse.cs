namespace Pharmacy.Application.Features.Groups.Commands.Create;

public sealed record CreatedGroupResponse(string Name, int Id, string Message="İşlem Başarılı");