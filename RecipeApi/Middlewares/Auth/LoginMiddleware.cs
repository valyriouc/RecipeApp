using System.Net;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Authentication;

using RecipeApi.Config;
using RecipeApi.Database;
using RecipeApi.Exceptions;
using RecipeApi.Extensions;
using RecipeApi.Json;

namespace RecipeApi.Middlewares;

public class LoginMiddleware {

    public RequestDelegate Next { get; set; }

    public IDbContext DbContext { get; set; }

    public LoginMiddleware(
        RequestDelegate next,
        IDbContext dbContext) {
        DbContext = dbContext;
        Next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) {

        if (httpContext.Request.Path != "/login") {
           await Next(httpContext);
           return;
        }

        if (httpContext.Request.Method != "POST") {
            throw HttpException.MethodNotAllowed();
        }

        LoginEntity? entity = httpContext.Request.BodyAsJson<LoginEntity>();
        
        if (entity?.Email is null || entity?.Password is null) {
            throw HttpException.BadRequest("Invalid user credentials!");
        }

        string query = $"SELECT * FROM User WHERE email={entity.Email}";

        IEnumerable<User> users = await DbContext
            .GetEntitiesAsync<User>(query);

        User? user = users.FirstOrDefault(
            user => user.Email == entity.Email);

        if (user is null) {
            throw HttpException.BadRequest(
                "Email or password are incorrect!");
        }

        byte[] encodedPassword = Encoding.UTF8.GetBytes(entity.Password);

        using SHA512 algSHA512 = SHA512.Create();

        byte[] hashedPassword = algSHA512.ComputeHash(encodedPassword);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hashedPassword.Length; i++)
        {
            sb.Append(hashedPassword[i].ToString("x2"));
        }
        
        string hashString = sb.ToString();  

        if (hashString != user.Password) {
            throw HttpException.BadRequest(
                "Email or password are incorrect!"
            );
        }

        await httpContext.SignInAsync(
            AuthenticationConfig.SchemaName,
            user.CreatePrinciple()
        );

        httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}
