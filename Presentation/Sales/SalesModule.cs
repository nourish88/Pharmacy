using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Sales.Commands.Create;
using Pharmacy.Application.Features.Sales.Commands.Create.SaleGroup;
using Pharmacy.Application.Features.Sales.Queries.Get;
using Pharmacy.Application.Features.Sales.Queries.GetAll;
using Pharmacy.Application.Features.Sales.Queries.GetById;

namespace Pharmacy.Presentation.Sales;

public class SalesModule:CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/sales", async ([FromBody]CreateSaleCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapPost("/sales-group", async ([FromBody] CreateSaleGroupCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapPost("/sales-group-query", async ([FromBody] GroupSaleQuery request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapPost("/sales-query", async ([FromBody] MedicineSaleQuery request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapGet("/sales/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetSaleResultByIdQuery { Id = id });
            if (result != null)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/sales", async ( ISender sender) =>
        {
            var result = await sender.Send(new GetAllSaleResultsQuery( ));
            if (result != null)
                return Results.Ok(result);
            return Results.NoContent();
        });
    }
}