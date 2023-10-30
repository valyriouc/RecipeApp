using System.Net;
using RecipeApi.Database;
using RecipeApi.Exceptions;

namespace RecipeApi.Middlewares;

public sealed class ExceptionMiddlware
{

    public RequestDelegate Next { get; set; }

    public ExceptionMiddlware(RequestDelegate next) 
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) 
    {
        try 
        {
            await Next(httpContext);
        }
        catch (HttpException ex) 
        {
            await HandleHttpException(httpContext, ex);
        }
        //catch (Exception) 
        //{
        //    await HandleException(httpContext);
        //}
    }

    private async Task HandleHttpException(HttpContext context, HttpException exception) 
    {
        ErrorResponse response = new ErrorResponse(exception);
        context.Response.StatusCode = (int)exception.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    private async Task HandleException(HttpContext context) 
    {
        ErrorResponse response = new ErrorResponse(
            HttpException.Custom(
                HttpStatusCode.InternalServerError, 
                "Something went wrong on the server!"));

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(response);
    }
}