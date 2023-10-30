using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

using RecipeApi.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Xunit.Sdk;

namespace RecipeApi.Tests.Helper.AuthenticationServices
{
    internal class StubAuthService : IAuthenticationService
    {
        private ClaimsPrincipal CreateDummyPrinciple()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();

            principal.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, "200"),
                new Claim(ClaimTypes.Name, "Musterlogin"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, "muster.login@gmail.com")
            }));

            return principal;
        }

        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
        {
            return Task.FromResult<AuthenticateResult>(AuthenticateResult.Success(
                new AuthenticationTicket(
                    CreateDummyPrinciple(), 
                    JwtBearerDefaults.AuthenticationScheme)));
        }

        public Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
        {
            throw HttpException.Custom(HttpStatusCode.Unauthorized, "You are not authenticated!");
        }

        public Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
        {
            throw HttpException.Custom(HttpStatusCode.Forbidden, "You are forbidden to login");
        }

        public Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
        {
            context.User = principal;
            return Task.CompletedTask;
        }

        public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
        {
            context.User = new ClaimsPrincipal();

            return Task.CompletedTask;
        }
    }
}
