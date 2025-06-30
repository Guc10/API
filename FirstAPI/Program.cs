var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<FirstAPI.Services.UserService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://tester-online.site")
            .WithHeaders("content-type", "X-API-KEY", "Accept")
            .WithMethods("POST", "GET", "PUT", "DELETE")
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

DotNetEnv.Env.Load();

app.UseCors();

app.Use(async (context, next) =>
{
    var apiKey = context.Request.Headers["X-API-KEY"];
    if (apiKey != Environment.GetEnvironmentVariable("X-API-KEY"))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid API Key");
        return;
    }
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();