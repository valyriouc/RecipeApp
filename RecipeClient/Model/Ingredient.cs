using RecipeClient.Model.InMemory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeClient.Model
{
    internal class Ingredient : ModelBase
    {
        private static int ObjectCounter { get; set; } = 0;

        public string Name { get; set; }

        public uint Amount { get; set; }

        private Ingredient(string name, uint amount)
        {
            ObjectCounter += 1;

            Id = ObjectCounter;
            Name = name.Trim();
            Amount = amount;
        }

        public void Update(string name, uint amount)
        {
            Name = name.ToString();
            Amount = amount;
        }

        public static Ingredient Create(string name, uint amount) =>
            new Ingredient(name, amount);

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode();
        }

        public bool Equals(InMemIngredient other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
    }
}
