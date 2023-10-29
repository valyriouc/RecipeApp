using System.Data.Common;

namespace RecipeApi.Database;

public interface IDbContext {

    public DbConnection Connection { get; }

    public string ConnectionString { get; }

    public void CreateConnection();

    public IEnumerable<TEntity> GetEntities<TEntity>(string query); 

    public Task<IEnumerable<TEntity>> GetEntitiesAsync<TEntity>(string query);

    public void ExecuteNonQuery(string query);

    public Task ExecuteNonQueryAsync(string query);
}