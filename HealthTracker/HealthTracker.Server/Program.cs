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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Security.Claims;
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
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "HealthTracker API",
            Version = "v1"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        string[] methodsOrder = new string[] { "get", "post", "put", "patch", "delete", "options", "trace" };
        c.OrderActionsBy(apiDesc =>
        {
            var methodIndex = Array.IndexOf(methodsOrder, apiDesc.HttpMethod?.ToLower() ?? "get");
            var debugInfo = $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{methodIndex}_{apiDesc.HttpMethod}_{apiDesc.RelativePath}";
            Console.WriteLine(debugInfo);
            return debugInfo;
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });

    });

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("HealthTrackerDBconnString")));
    builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
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
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
        googleOptions.Events.OnCreatingTicket = context =>
        {
            Log.Information("Użytkownik zalogowany przez Google: {Email}", context.Principal.FindFirst(ClaimTypes.Email).Value);
            return Task.CompletedTask;
        };
    });
}

void ConfigureCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder => builder
            .WithOrigins("https://localhost:5174")
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

    app.UseHsts();
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
