using FirebaseAdmin;
using FitAppServer.DataAccess;
using FitAppServer.Services;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Check if this actually works
if (builder.Configuration.GetConnectionString("postgres") != null)
{
    builder.Services.AddDbContext<FitAppContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));
}
else
{
    // Pass empty connection string to enable EFBundle to work
    // https://github.com/dotnet/efcore/issues/27325#issuecomment-1028795149
    builder.Services.AddDbContext<FitAppContext>(options => options.UseNpgsql(""));
}

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IWorkoutsService, WorkoutsService>();

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(builder.Configuration["Firebase:CredentialsPath"])
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