using System.Data.Common;

namespace RecipeApi.Database.Entities
{
    public interface IDatabaseReadable<TEntity> 
        where TEntity : new()
    {
        public static abstract TEntity CreateFrom(DbDataReader reader);
    }
}
