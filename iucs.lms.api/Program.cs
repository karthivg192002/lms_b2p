using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using iucs.lms.domain.Data;
using iucs.lms.domain.Repositories;
using iucs.lms.api.Mappings;
using iucs.lms.api.Services;
using iucs.lms.application.Services;
using iucs.lms.application.Helper.Middleware;
using iucs.lms.application.Helper.DataSeeder;
using iucs.lms.domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var lmsConnection = builder.Configuration.GetValue<string>("LMSDESIGN");

    if (string.IsNullOrWhiteSpace(lmsConnection))
        throw new Exception("LMSDESIGN environment variable is not set");

    options.UseNpgsql(lmsConnection);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Routing
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("lmsCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await DataSeeder.SeedAsync(
        services.GetRequiredService<IRepository<Role>>(),
        services.GetRequiredService<IRepository<Menu>>(),
        services.GetRequiredService<IRepository<User>>(),
        services.GetRequiredService<IRepository<UserRole>>(),
        services.GetRequiredService<IRepository<RoleMenu>>()
    );
}

// Development tools
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    //app.MapScalarApiReference(options =>
    //{
    //    options.Title = "IUCS LMS API";
    //    options.Theme = ScalarTheme.BluePlanet;
    //});
}
app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "IUCS LMS API";
    options.Theme = ScalarTheme.BluePlanet;
});

app.UseHttpsRedirection();

app.UseCors("lmsCors");

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<PermissionMiddleware>();

app.MapControllers();

app.Run();