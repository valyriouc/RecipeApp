using System;
using System.Collections.Generic;

namespace RecipeClient.Database.Core
{
    internal interface IRepository<TEntity> 
        where TEntity : class
    {
        public TEntity GetSingle(Func<TEntity, bool> filter);

        public IEnumerable<TEntity> Get(Func<TEntity, bool> filter);

        public void Create(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(int id);
    }
}
