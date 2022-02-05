FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

# PATH was changed in the run configuration
# It was set to the solution folder
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FitAppServer.Auth/FitAppServer.Auth.csproj", "FitAppServer.Auth/"]
COPY ["FitAppServer.Services/FitAppServer.Services.csproj", "FitAppServer.Services/"]
RUN dotnet restore "FitAppServer.Auth/FitAppServer.Auth.csproj"
COPY . .
WORKDIR "/src/FitAppServer.Auth"
RUN dotnet build "FitAppServer.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitAppServer.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=https://+:5001
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitAppServer.Auth.dll"]
