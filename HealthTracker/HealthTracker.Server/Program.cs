using HealthTracker.Server.Core.Models;
using HealthTracker.Server.Core.Repositories;
using HealthTracker.Server.Infrastructure.Hubs;
using HealthTracker.Server.Infrastrucure.Data;
using HealthTracker.Server.Modules.Community.Controllers;
using HealthTracker.Server.Modules.Community.Helpers;
using HealthTracker.Server.Modules.Community.Repositories;
using HealthTracker.Server.Modules.PhysicalActivity.Helpers;
using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder);
ConfigureServices(builder);
ConfigureAuthentication(builder);
ConfigureCors(builder);

var app = builder.Build();
ConfigureMiddleware(app);
ConfigureEndpoints(app);

app.Run();

void ConfigureLogging(WebApplicationBuilder builder)
{
    var logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}


void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
    });
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("HealthTrackerDBconnString")));
    builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });

    // Dependency Injection for Repositories and AutoMapper
    AddRepositoryServices(builder);
    AddAutoMapperProfiles(builder);

    builder.Services.AddSignalR();
}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });
}

void ConfigureCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder => builder
            .WithOrigins("https://localhost:5173", "https://localhost:5174", "https://localhost:5175")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
    });
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCors("AllowSpecificOrigin");
    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(WebApplication app)
{
    app.MapControllers();
    app.MapHub<ChatHub>("/chatHub");
    app.MapFallbackToFile("/index.html");
}

void AddRepositoryServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IChatRepository, ChatRepository>();
    builder.Services.AddScoped<IFriendRepository, FriendshipRepository>();
    builder.Services.AddScoped<IPostRepository, PostRepository>();
    builder.Services.AddScoped<IStatusRepository, StatusRepository>();
    builder.Services.AddScoped<IGoalRepository, GoalRepository>();
    builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
    builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
}

void AddAutoMapperProfiles(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(typeof(UserProfile));
    builder.Services.AddAutoMapper(typeof(ChatProfile));
    builder.Services.AddAutoMapper(typeof(FriendshipProfile));
    builder.Services.AddAutoMapper(typeof(PostProfile));
    builder.Services.AddAutoMapper(typeof(GoalProfile));
    builder.Services.AddAutoMapper(typeof(ExerciseProfile));
    builder.Services.AddAutoMapper(typeof(WorkoutProfile));
}
