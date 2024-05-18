using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Customers.Commands.Create;
using Pharmacy.Application.Features.Customers.Commands.Delete;
using Pharmacy.Application.Features.Customers.Commands.Update;
using Pharmacy.Application.Features.Customers.Queries.Get;
using Pharmacy.Application.Features.Customers.Queries.GetAll;
using Pharmacy.Application.Features.Customers.Queries.GetById;
using Pharmacy.Application.Features.Customers.Queries.GetByName;

namespace Pharmacy.Presentation.Customers;

public class CustomersModule : CarterModule

{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/customer/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetByIdCustomerQuery { Id = id });
            if (result != null)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/customers/all", async ( ISender sender) =>
        {
            var result = await sender.Send(new GetAllCustomersQuery ());
            if (result.Customers.Any())
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapGet("/customers", async (ISender sender) =>
        {
            var result = await sender.Send(new GetBaseCustomersQuery());
            if (result.Customers.Any())
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapPost("/get-customers", async ([FromBody] GetCustomersQuery request, ISender sender) =>
        { var result = await sender.Send(request);
            
                return Results.Ok(result);
          
        });
        app.MapGet("/customers/{name}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new GetByNameCustomerQuery { Name = name });
            if (result.Customers.Count > 0)
                return Results.Ok(result);
            return Results.NoContent();
        });
        app.MapPost("/customers", async ([FromBody]CreateCustomerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
           
            return Results.Created($"customer/{result.Id}", result);
        });
        app.MapPut("/customers", async ([FromBody]UpdateCustomerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        app.MapDelete("/customers", async ([FromBody]DeleteCustomerCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
    }
}