using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeClient.Model.InMemory;

public sealed class InMemRecipe
{
    private static int ObjectCounter { get; set; } = 0;

    public int Id { get; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; }

    public string? Description { get; set; }

    public InMemUser User { get; }

    private InMemRecipe(string title, string? description, InMemUser user)
    {
        ObjectCounter += 1;

        Id = ObjectCounter;
        Title = title;
        CreatedAt = DateTime.Now; // This has to be UTC for web api 
        Description = description;
        User = user;
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

