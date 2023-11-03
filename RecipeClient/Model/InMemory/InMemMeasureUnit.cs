namespace RecipeClient.Model.InMemory;

public struct InMemMeasureUnit
{
    private static int ObjectCounter { get; set; } = 0;

    public int Id { get; }

    public string Name { get; }

    private InMemMeasureUnit(string name)
    {
        ObjectCounter += 1;

        Id = ObjectCounter;
        Name = name.Trim();
    }

    public static InMemMeasureUnit Create(string name) =>
        new InMemMeasureUnit(name);
}

