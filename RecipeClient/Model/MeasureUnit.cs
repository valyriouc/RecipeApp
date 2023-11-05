namespace RecipeClient.Model;

public class MeasureUnit : ModelBase
{
    private static int ObjectCounter { get; set; } = 0;

    public string Name { get; }

    private MeasureUnit(string name)
    {
        ObjectCounter += 1;

        Id = ObjectCounter;
        Name = name.Trim();
    }

    public static MeasureUnit Create(string name) =>
        new MeasureUnit(name);
}
