using System.Net;
using Microsoft.AspNetCore.Http;

namespace RecipeApi.Helper;

// internal sealed class HttpContextBuilder {

//     private HttpRequest Request { get; set; }

//     private HttpResponse Response { get; set; }

//     public HttpContextBuilder() {

//     }

//     private void ResetBuilder() {
//         Request = null;
//         Response = null;
//     }

//     public HttpContext Build() {
//         HttpRequest request = Request;
//         HttpResponse response = Response;

//         ResetBuilder();
    
//         DefaultHttpContext context = new DefaultHttpContext();
//     }
// }