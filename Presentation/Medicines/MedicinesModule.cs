using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Application.Features.Customers.Commands.Delete;
using Pharmacy.Application.Features.Customers.Commands.Update;
using Pharmacy.Application.Features.Customers.Queries.GetAll;
using Pharmacy.Application.Features.Customers.Queries.GetById;
using Pharmacy.Application.Features.Customers.Queries.GetByName;
using Pharmacy.Application.Features.Medicines.Commands.Create;
using Pharmacy.Application.Features.Medicines.Commands.Delete;
using Pharmacy.Application.Features.Medicines.Commands.Update;
using Pharmacy.Application.Features.Medicines.Queries.GetAll;
using Pharmacy.Application.Features.Medicines.Queries.GetById;
using Pharmacy.Application.Features.Medicines.Queries.GetByName;

namespace Pharmacy.Presentation.Medicines;

public class MedicinesModule : CarterModule

{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/medicines/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetByIdMedicineQuery { Id = id });
            if (result != null)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/medicines", async ( ISender sender) =>
        {
            var result = await sender.Send(new GetAllMedicinesQuery ());
            if (result.Medicines.Any())
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/medicines/{name}/{groupName}/{skip}/{take}", async ([FromRoute]string name, [FromRoute] string groupName, [FromRoute] int skip , [FromRoute] int take , ISender sender) =>
        {
            var result = await sender.Send(new GetByNameMedicineQuery { Name = name,GroupName = groupName,Skip = skip, Take = take});
            
                return Results.Ok(result);
          
        });
        app.MapPost("/medicines", async ([FromBody]CreateMedicineCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapPut("/medicines", async ([FromBody]UpdateMedicineCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapDelete("/medicines", async ([FromBody]DeleteMedicineCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
    }
}