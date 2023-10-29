
using System.Net;
using System.Text.Json.Serialization;
using RecipeApi.Exceptions;

namespace RecipeApi.Database;

public class ErrorResponse {

    [JsonPropertyName("statusCode")]
    public HttpStatusCode StatusCode { get; set;}

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }

    // Required for JSON serialization 
    public ErrorResponse() {

    }

    public ErrorResponse(HttpException exception) {
        StatusCode = exception.StatusCode;
        Errors = new List<string>();

        Errors.Add(exception.Message);
    }
}