using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;

using RecipeApi.Database;
using RecipeApi.Middlewares;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme, 
        options =>
        {

        });

builder.Services.AddScoped<IDbContext, RecipeDbContext>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.UseMiddleware<ExceptionMiddlware>();
app.UseMiddleware<LoginMiddleware>();

app.UseAuthentication();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
