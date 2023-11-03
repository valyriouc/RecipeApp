using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeClient.Model.InMemory;

public readonly struct InMemIngredMeasureUnit : IEquatable<InMemIngredMeasureUnit>
{
    public InMemIngredient Ingredient { get; }

    public InMemMeasureUnit MeasureUnit { get; }

    public InMemIngredMeasureUnit(InMemIngredient ingredient, InMemMeasureUnit measureUnit)
    {
        Ingredient = ingredient;
        MeasureUnit = measureUnit;
    }

    public bool Equals(InMemIngredMeasureUnit other)
    {
        return Ingredient.Equals(other.Ingredient);
    }
}

public sealed class InMemRecipe
{
    private static int ObjectCounter { get; set; } = 0;

    public HashSet<InMemIngredMeasureUnit> Ingredients { get; set; }

    public int Id { get; }

    public string Title { get; private set; } = null!;

    public DateTime CreatedAt { get; }

    public string? Description { get; private set; }

    public InMemUser User { get; }

    private InMemRecipe(string title, string? description, InMemUser user)
    {
        ObjectCounter += 1;

        Id = ObjectCounter;
        Title = title;
        CreatedAt = DateTime.Now; // This has to be UTC for web api 
        Description = description;
        User = user;
        Ingredients = new HashSet<InMemIngredMeasureUnit>();
    }
    
    public void Update(string title, string? description)
    {
        if (!IsValidTitle(title))
        {
            throw new Exception(
                "Something with the updating of the recipe went wrong (replace with application exception)");
        }

        Title = title.Trim();
        Description = description?.Trim();
    }

    public static bool IsValidTitle(string title) =>
        !string.IsNullOrWhiteSpace(title) && Validator.TryValidateValue(
            title,
            new ValidationContext(title),
            new List<ValidationResult>(),
            new List<ValidationAttribute>()
            {
                new RequiredAttribute(),
                new MaxLengthAttribute(100)
            });

    public static InMemRecipe Create(
        string title, 
        string? description, 
        InMemUser user)
    {
        if (!IsValidTitle(title))
        {
            throw new Exception(
                "Something went wrong in the creation of a recipe (Replace with application exception)");
        }

        return new InMemRecipe(
            title.Trim(), 
            description?.Trim(), 
            user);
    }
}

