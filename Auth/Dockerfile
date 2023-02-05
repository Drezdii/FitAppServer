﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
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
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitAppServer.Auth.dll"]