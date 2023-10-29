
using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace RecipeApi.Database;

internal class RecipeDbContext : IDbContext
{
    public DbConnection Connection { get; }

    public string ConnectionString => "Server=localhost;Database=recipeappdb;Uid=vector;Pwd=K/]zjUT)({?Xbdy?<+YEpsNzB38,*0$rc7DiAqvL";

    public RecipeDbContext() {
        Connection = new MySqlConnection(ConnectionString);

        CreateConnection();
    }

    public void CreateConnection()
    {
        if (Connection.State != ConnectionState.Open) {
            Connection.Open();
        }
    }

    public void ExecuteNonQuery(string query)
    {
        using DbCommand command = new MySqlCommand(query);
        command.ExecuteNonQuery();
    }

    public async Task ExecuteNonQueryAsync(string query)
    {
        using DbCommand command = new MySqlCommand(query);
        await command.ExecuteNonQueryAsync();
    }

    public IEnumerable<TEntity> GetEntities<TEntity>(string query)
    {
        using DbCommand command = new MySqlCommand(query);

        using DbDataReader reader = command.ExecuteReader();

        return reader.OfType<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TEntity>(string query)
    {
        using DbCommand command = new MySqlCommand(query);

        using DbDataReader reader = await command.ExecuteReaderAsync();

        return reader.OfType<TEntity>();
    }
}