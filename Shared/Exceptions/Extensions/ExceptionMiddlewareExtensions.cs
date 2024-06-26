﻿namespace Pharmacy.Shared.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        =>app.UseMiddleware<ExceptionMiddleware>();
}
