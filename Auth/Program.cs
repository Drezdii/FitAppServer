using FirebaseAdmin;
using FitAppServer.DataAccess;
using FitAppServer.Services.Services;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FitAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IWorkoutsService, WorkoutsService>();

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("firebase_private_key.json")
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = "https://securetoken.google.com/fitapp-68345";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = "https://securetoken.google.com/fitapp-68345",
        ValidAudience = "fitapp-68345"
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();