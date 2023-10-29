
using System.Net;
using System.Text;

using Microsoft.AspNetCore.Http;
using RecipeApi.Exceptions;
using RecipeApi.Middlewares;
using RecipeApi.Tests.Database;
using Xunit;

namespace RecipeApi.Tests.Middlewares;

public class LoginMiddlwareShould {

    [Fact]
    public async Task CallNextMiddlewareWhenWrongRequestPath() {
        
        bool isCalled = false;
        LoginMiddleware middleware = new LoginMiddleware(async (HttpContext _ ) => {
            isCalled = true;
            await Task.CompletedTask;
        },
        new AuthDbContext());
        
        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/register";

        await middleware.InvokeAsync(context);

        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)context.Response.StatusCode);
        Assert.True(isCalled);
    }

    [Fact]
    public async Task ThrowExceptionWhenWrongMethodIsUsed() {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext());

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "GET";

        await Assert.ThrowsAsync<HttpException>(() => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ThrowsExceptionWhenNotAbleToParseCredentials() {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext()
        );

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload = 
            """
            {
                "testing": "hello world",
                "pawd": 23234,
                "nice": 23
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        await Assert.ThrowsAsync<HttpException>(
            () => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ThrowsExceptionWhenPasswordIsNotPresent()
    {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext()
        );

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload =
            """
            {
                "email": "hello",
                "testing": "23032"
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        await Assert.ThrowsAsync<HttpException>(
            () => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ThrowsExceptionWhenEmailIsNotPresent()
    {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext()
        );

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload =
            """
            {
                "nice": "hello",
                "password": "23032"
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        await Assert.ThrowsAsync<HttpException>(
            () => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ThrowsExceptionWhenUserDoesNotExists() {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext()
        );

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload = 
            """
            {
                "email": "hello.testing@gmail.com",
                "password: "Nice123"
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        await Assert.ThrowsAsync<HttpException>(
            () => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ThrowsExceptionWhenPasswordForCorrectUserIsWrong() {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext()
        );
        
        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload = 
            """
            {
                "email": "max.mustermann@gmail.com",
                "password": "testing123"
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        await Assert.ThrowsAsync<HttpException>(
            () => middleware.InvokeAsync(context));
    }

    [Fact]
    public async Task ReturnsStatusCodeOkAndSignsInTheUser()
    {
        LoginMiddleware middleware = new LoginMiddleware(
            async (HttpContext _) => await Task.CompletedTask,
            new AuthDbContext());

        HttpContext context = new DefaultHttpContext();

        context.Request.Path = "/login";
        context.Request.Method = "POST";
        context.Request.Body = new MemoryStream();

        string payload =
            """
            {
                "email": "max.mustermann@gmail.com",
                "password": "password123"
            }
            """;

        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        context.Request.Body.Write(bytes);

        context.RequestServices.AddAuthentication
        await middleware.InvokeAsync(context);

        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)context.Response.StatusCode);
    }
}