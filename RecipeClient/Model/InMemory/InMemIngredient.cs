using System;

namespace RecipeClient.Model.InMemory;

public struct InMemIngredient : IEquatable<InMemIngredient>
{
    private static int ObjectCounter { get; set; } = 0;

    public int Id { get; }

    public string Name { get; set; }

    public uint Amount { get; set; }

    private InMemIngredient(string name, uint amount)
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

    public static InMemIngredient Create(string name, uint amount) =>
        new InMemIngredient(name, amount);

    public override int GetHashCode()
    {
        return Id.GetHashCode() + Name.GetHashCode();
    }

    public bool Equals(InMemIngredient other)
    {
        return this.GetHashCode() == other.GetHashCode();   
    }
}

