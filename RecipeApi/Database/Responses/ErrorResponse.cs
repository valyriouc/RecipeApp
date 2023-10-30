
using System.Net;
using System.Text.Json.Serialization;
using RecipeApi.Exceptions;

namespace RecipeApi.Database;

public class ErrorResponse 
{

    [JsonPropertyName("statusCode")]
    public HttpStatusCode StatusCode { get; set;}

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }

    // Required for JSON serialization 
    public ErrorResponse() 
    {
        Errors = new List<string>();
    }

    public ErrorResponse(HttpException exception) 
    {
        StatusCode = exception.StatusCode;
        Errors = new List<string>();

        if (!string.IsNullOrEmpty(exception.Message))
        {
            Errors.Add(exception.Message);
        }
    }
}