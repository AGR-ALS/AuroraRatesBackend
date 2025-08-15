using AuroraRates.Application.Services;
using AuroraRates.DataAccess;
using AuroraRates.DataAccess.Repository;
using AuroraRates.Infrastructure.Authentication;
using AuroraRates.Infrastructure.Files;
using AuroraRates.Api.ApiExtensions;
using AuroraRates.Api.Middleware;
using AuroraRates.Application.Abstractions.Authentication;
using AuroraRates.Application.Abstractions.Files;
using AuroraRates.Domain.Abstractions.JWT;
using AuroraRates.Domain.Abstractions.MediaType;
using AuroraRates.Domain.Abstractions.Reviews;
using AuroraRates.Domain.Abstractions.Users;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builderOptions = new WebApplicationOptions
{
    WebRootPath = "wwwroot"
};

var builder = WebApplication.CreateBuilder(builderOptions);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AuroraRatesDatabaseContext>();

builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaTypeService, MediaTypeService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserDataService, CurrentUserDataService>();

builder.Services.AddScoped<ImageUploader, ImageUploader>();
builder.Services.AddScoped<IFileDeletingService, FileDeletingService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddAppAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();   
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1"));
    app.MapScalarApiReference();
    Console.WriteLine(app.Environment.EnvironmentName);
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AuroraRatesDatabaseContext>();
        db.Database.Migrate();
    }
}

app.UseMiddleware<ExceptionHandler>();
app.UseHttpsRedirection();
app.UseCors("AllowNextApp");
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
