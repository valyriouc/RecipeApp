using RecipeApi.Database;
using RecipeApi.Middlewares;

using System.Reflection.Metadata.Ecma335;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddAuthentication()
    .AddCookie();

builder.Services.AddScoped<RecipeDbContext>();

WebApplication app = builder.Build();

app.UseMiddleware<LoginMiddleware>();

app.UseAuthentication();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
