using System.IdentityModel.Tokens.Jwt;
using FitAppServer.DataAccess;
using FitAppServer.Resolvers;
using FitAppServer.Services;
using FitAppServer.Types;
using FitAppServer.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            ValidateAudience = true,
        };
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddTransient<IWorkoutsService, WorkoutsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<WorkoutsResolver>();
builder.Services.AddTransient<WorkoutMutation>();
builder.Services.AddScoped<IClaimsAccessor, ClaimsAccessor>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddPooledDbContextFactory<FitAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddDocumentFromFile("schema.graphql")
    .AddResolver<WorkoutsResolver>("WorkoutType")
    .AddResolver<WorkoutsResolver>("ExerciseType")
    .AddResolver<WorkoutsResolver>("SetType")
    .AddResolver<WorkoutsResolver>("Query")
    .AddResolver<WorkoutMutation>("Mutation")
    .BindRuntimeType<WorkoutInput>()
    .BindRuntimeType<ExerciseInput>()
    .BindRuntimeType<SetInput>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.Run();