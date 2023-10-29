
using System.Data.Common;
using RecipeApi.Database;

namespace RecipeApi.Tests.Database;

internal class AuthDbContext : IDbContext
{   
    public DbConnection Connection => throw new NotImplementedException();

    public string ConnectionString => throw new NotImplementedException();

    public void CreateConnection()
    {
        throw new NotImplementedException();
    }

    public void ExecuteNonQuery(string query)
    {
        throw new NotImplementedException();
    }

    public Task ExecuteNonQueryAsync(string query)
    {
        throw new NotImplementedException();
    }

    List<User> Users { get; set; } = new List<User>() {
        new User() {
            Id = 0,
            Email = "max.mustermann@gmail.com",
            Username = "maxmuster",
            Role = UserRoles.User,
            Password = "bed4efa1d4fdbd954bd3705d6a2a78270ec9a52ecfbfb010c61862af5c76af1761ffeb1aef6aca1bf5d02b3781aa854fabd2b69c790de74e17ecfec3cb6ac4bf"
        }
    };


    public IEnumerable<User> GetEntities<User>(string query)
    {
        return (IEnumerable<User>)Users;
    }

    public Task<IEnumerable<User>> GetEntitiesAsync<User>(string query)
    {
        return Task.Run(() => (IEnumerable<User>)Users);
    }
}