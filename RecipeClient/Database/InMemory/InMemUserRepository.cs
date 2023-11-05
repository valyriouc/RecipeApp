using RecipeClient.Database.Core;
using RecipeClient.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeClient.Database.InMemory
{
    internal class InMemUserRepository : IRepository<User>
    {
        public List<User> Users { get; }

        public InMemUserRepository()
        {
            Users = new List<User>();
            Initialize();
        }

        private void Initialize()
        {
            User user1 = User.Create("test", "test.tester@gmail.com", "password12345");

            Users.Add(user1);
        }

        public User GetSingle(Func<User, bool> filter)
        {
            User? user = Users.FirstOrDefault(filter);

            if (user is null)
            {   
                throw new Exception(
                    "User does not exists! (Application exception)");
            }

            return user;
        }

        public IEnumerable<User> Get(Func<User, bool> filter)
        {
            return Users.Where(filter);
        }

        public void Create(User entity)
        {
            Users.Add(entity);
        }

        public void Update(User entity)
        {
            int index = Users.IndexOf(entity);

            if (index == -1)
            {
                throw new Exception(
                    "User does not exists! (Application exception)");
            }

            Users[index] = entity;
        }

        public void Delete(int id)
        {
            User user = GetSingle(x => x.Id == id);
            Users.Remove(user);
        }
    }
}
