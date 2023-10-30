using Xunit;
using RecipeApi.Middlewares;
using RecipeApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using RecipeApi.Database;
using Microsoft.AspNetCore.Diagnostics;

namespace RecipeApi.Tests.Middlewares;

public sealed class ExceptionMiddlwareShould {

    [Fact]
    public async Task CatchHttpExceptionAndReturnAppropriateResponseWithCorrectStatusCode() 
    {
        
        ExceptionMiddlware middlware = new ExceptionMiddlware(
            (HttpContext _ ) => throw HttpException.MethodNotAllowed());

        HttpContext context = new DefaultHttpContext();

        await middlware.InvokeAsync(context);

        Assert.Equal(
            HttpStatusCode.MethodNotAllowed, 
            (HttpStatusCode)context.Response.StatusCode);
    }

    [Fact]
    public async Task CatchHttpExceptionAndReturnAppropriateResponseWithCorrectStatusCodeAndMessage() 
    {
        ExceptionMiddlware middleware = new ExceptionMiddlware(
            (HttpContext _) => throw HttpException.BadRequest("This is a exception"));

        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.InvokeAsync(context);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            (HttpStatusCode)context.Response.StatusCode);

        context.Response.Body.Position = 0;
        ErrorResponse? res = JsonSerializer.Deserialize<ErrorResponse>(
            context.Response.Body);

        Assert.NotNull(res);
        Assert.Single(res.Errors);
        Assert.Equal("This is a exception", res.Errors[0]);
    } 

    [Fact]
    public async Task CatchNotFoundExceptionAndReturnsAppropriateResponse()
    {
        ExceptionMiddlware middleware = new ExceptionMiddlware(
            (HttpContext _) => throw HttpException.NotFound("This is a exception"));
        
        HttpContext context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.InvokeAsync(context);

        Assert.Equal(
            HttpStatusCode.NotFound, 
            (HttpStatusCode)context.Response.StatusCode
        );

        context.Response.Body.Position = 0;
        ErrorResponse? res = JsonSerializer.Deserialize<ErrorResponse>(context.Response.Body);

        Assert.NotNull(res);
        Assert.Single(res.Errors);
        Assert.Equal("This is a exception", res.Errors[0]);
    }
    
    [Fact]
    public async Task CatchExceptionAndReturnInternalServerErrorStatusCode() 
    {
        ExceptionMiddlware middleware = new ExceptionMiddlware(
            (HttpContext _) => throw new Exception("Something went wrong")
        );

        HttpContext context = new DefaultHttpContext();

        await middleware.InvokeAsync(context);

        Assert.Equal(
            HttpStatusCode.InternalServerError,
            (HttpStatusCode)context.Response.StatusCode
        );
    }       
}