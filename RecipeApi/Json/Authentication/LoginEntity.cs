
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeApi.Json;

internal class LoginEntity {

    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set;} = null!;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
}