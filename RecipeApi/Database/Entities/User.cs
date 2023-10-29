using System.Security.Claims;

namespace RecipeApi.Database;

public enum UserRoles {
    User = 0,
    Admin = 1,
}

public class User {
    
    public int Id { get; set; }

    public string Username { get; set; }

    public UserRoles Role { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }

    public User() {

    }
    
    public ClaimsPrincipal CreatePrinciple() => 
        new ClaimsPrincipal(
            new ClaimsIdentity(
                new List<Claim>() {
                    new Claim(ClaimTypes.Sid, Id.ToString()),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, Role.ToString())   
                }
            )
        );
    
}