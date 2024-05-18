
using System.Reflection;
using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure;
using Pharmacy.Presentation.Customers;
using Pharmacy.Shared.Exceptions.Extensions;
using Pharmacy.Shared.Pipelines.Validation;

namespace Pharmacy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddCarter();
            builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
                    opt.EnableSensitiveDataLogging(true);
                });
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                configuration.AddOpenBehavior(typeof(RequestValidation.RequestValidationBehavior<,>));
                // configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
            });
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            //builder.Host.UseWindowsService();
            //builder.Services.AddWindowsService();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfigureCustomExceptionMiddleware();

            app.MapCarter();




            app.UseCors("AllowAllOrigins");


            app.Run();
        }
    }
}
