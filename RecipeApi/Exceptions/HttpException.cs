using System.Net;

namespace RecipeApi.Exceptions;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    private HttpException(HttpStatusCode statusCode, string message) 
        : base(message) { 
            StatusCode = statusCode;
        }

    public static HttpException BadRequest(string message) {
        return new HttpException(
            HttpStatusCode.BadRequest, 
            message);
    }

    public static HttpException MethodNotAllowed() {
        return new HttpException(
            HttpStatusCode.MethodNotAllowed, 
            string.Empty);
    }

    public static HttpException NotFound(string message) {
        return new HttpException(
            HttpStatusCode.NotFound,
            message);
    }

    public static HttpException Custom(
        HttpStatusCode statusCode, 
        string message) {
            return new HttpException(statusCode, message);
        }
}