using Pharmacy.Shared.Exceptions.Handlers;

namespace Pharmacy.Shared.Exceptions;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly HttpExceptionHandler _httpExceptionHandler = new();
 

    public async Task Invoke(HttpContext context) 
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            
            await HandleExceptionAsync(context.Response, exception);
        }
    
    }

 

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
