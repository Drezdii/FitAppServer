using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using FitAppServer.DataAccess;
using FitAppServer.Services;
using FitAppServerREST.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/fitapp-68345";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "https://securetoken.google.com/fitapp-68345",
            ValidAudience = "fitapp-68345",
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = true
        };
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


builder.Services.AddAuthorization();

builder.Services.AddScoped<IWorkoutsService, WorkoutsService>();
builder.Services.AddScoped<IAchievementsService, AchievementsService>();
builder.Services.AddScoped<IAchievementsManager, AchievementsManager>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddDbContext<FitAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddControllers();

builder.Services.AddMvc().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.AddDateOnlyConverters();
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();