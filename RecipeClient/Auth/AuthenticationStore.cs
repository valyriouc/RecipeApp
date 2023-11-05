using RecipeClient.Model;

using System.Collections.Generic;
using System.Security.Claims;

namespace RecipeClient.Auth
{
    internal class AuthenticationStore
    {
        private static ClaimsPrincipal? Principal { get; set; }

        public static void Add(User user)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();

            principal.AddIdentity(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            }));
        }

        public static void Remove()
        {
            Principal = null;
        }
    }
}
