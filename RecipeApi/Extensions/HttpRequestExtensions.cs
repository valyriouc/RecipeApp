using System.Text.Json;
using RecipeApi.Exceptions;

namespace RecipeApi.Extensions;

internal static class HttpRequestExtensions {

    public static TEntity? BodyAsJson<TEntity>(this HttpRequest request) {
        Stream body = request.Body;
        body.Position = 0;
        JsonSerializerOptions options = new() {
            PropertyNameCaseInsensitive = true
        };

        try {
            TEntity? entity = JsonSerializer
                .Deserialize<TEntity>(body, options);
            return entity;
        }
        catch (JsonException) {
            throw HttpException.BadRequest("Malformed request body!");
        }
    }
}