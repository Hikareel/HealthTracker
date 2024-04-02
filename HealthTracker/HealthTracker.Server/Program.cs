using HealthTracker.Server.Infrastrucure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using HealthTracker.Server.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using HealthTracker.Server.Core.Repositories;
using HealthTracker.Server.Modules.PhysicalActivity.Repository;
using HealthTracker.Server.Modules.PhysicalActivity.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HealthTrackerDBconnString")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodaj uwierzytelnianie JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.Configure<SignInOptions>(options =>
{
    options.RequireConfirmedEmail = false;
    options.RequireConfirmedPhoneNumber = false;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

builder.Services.AddAutoMapper(typeof(GoalProfile));


builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));

app.MapFallbackToFile("/index.html");

app.Run();
