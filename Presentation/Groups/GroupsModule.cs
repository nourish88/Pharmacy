using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Groups.Commands.Create;
using Pharmacy.Application.Features.Groups.Commands.Delete;
using Pharmacy.Application.Features.Groups.Queries;
using Pharmacy.Application.Features.Medicines.Commands.Delete;


namespace Pharmacy.Presentation.Groups;

public class GroupsModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/groups/{name}/{skip}/{take}", async ([FromRoute] string name, [FromRoute] int skip, [FromRoute] int take, ISender sender) =>
        {
            var result = await sender.Send(new GetGroupsQuery {Name=name, Skip = skip, Take = take });
            if (result.Groups.Count > 0)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/groups", async ( ISender sender) =>
        {
            var result = await sender.Send(new GetAllGroupsQuery {  });
            if (result.Groups.Count > 0)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapPost("/groups", async ([FromBody] CreateGroupCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapDelete("/groups", async ([FromBody] DeleteGroupCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
    }
}