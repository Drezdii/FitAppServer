using System;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Resolvers;
using FitAppServer.Services;
using FitAppServer.Types;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<FitAppContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddPooledDbContextFactory<FitAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddTransient<IWorkoutsService, WorkoutsService>();

// builder.Services
//     .AddGraphQLServer()
//     .AddQueryType<Query>();

builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("schema.graphql")
    // .BindRuntimeType<Query>()
    // .BindRuntimeType<WorkoutType>()
    .AddResolver<WorkoutsResolver>("WorkoutType")
    .AddResolver<WorkoutsResolver>("Query");

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

// app.MapControllers();

app.MapGraphQL();

app.Run();