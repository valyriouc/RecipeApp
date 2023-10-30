
using System.Collections;
using System.Data;
using System.Data.Common;
using MySqlConnector;

using RecipeApi.Database.Entities;

namespace RecipeApi.Database;

internal class RecipeDbContext : IDbContext, IDisposable
{
    public DbConnection Connection { get; }

    private DbDataReader? DbReader { get; set; }

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
        using DbCommand command = Connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
    }

    public async Task ExecuteNonQueryAsync(string query)
    {
        using DbCommand command = Connection.CreateCommand();
        command.CommandText = query;
        await command.ExecuteNonQueryAsync();
    }

    public IEnumerable<TEntity> GetEntities<TEntity>(string query)
        where TEntity : IDatabaseReadable<TEntity>, new()
    {
        using DbCommand command = Connection.CreateCommand();

        command.CommandText = query;

        DbReader = command.ExecuteReader();

        do
        {
            yield return TEntity.CreateFrom(DbReader);
        } while (DbReader.NextResult());
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TEntity>(string query)
        where TEntity : IDatabaseReadable<TEntity>, new() 
    {
        using DbCommand command = Connection.CreateCommand();

        command.CommandText = query;

        DbReader = await command.ExecuteReaderAsync();

        List<TEntity> entities = new List<TEntity>();

        do
        {
            entities.Add(TEntity.CreateFrom(DbReader));
        } while (DbReader.NextResult());

        return entities;
    }

    private bool IsDisposed { get; set; } = false;

    public void Dispose()
    {
        if (!IsDisposed || Connection is null)
        {
            return;
        }

        Connection.Dispose();
        DbReader?.Close();
        IsDisposed = true;
    }
}   