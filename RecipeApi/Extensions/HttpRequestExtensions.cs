using System.Text.Json;
using RecipeApi.Exceptions;

namespace RecipeApi.Extensions;

internal static class HttpRequestExtensions 
{

    public static async Task<TEntity?> BodyAsJsonAsync<TEntity>(this HttpRequest request) 
    {
        Stream body = request.Body;
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        try 
        {
            TEntity? entity = await JsonSerializer
                .DeserializeAsync<TEntity>(body, options);
            return entity;
        }
        catch (JsonException) 
        {
            throw HttpException.BadRequest("Malformed request body!");
        }
    }
}