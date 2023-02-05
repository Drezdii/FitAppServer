FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FitAppServerREST/FitAppServerREST.csproj", "FitAppServerREST/"]
COPY ["FitAppServer.Services/FitAppServer.Services.csproj", "FitAppServer.Services/"]
COPY ["FitAppServer.DataAccess/FitAppServer.DataAccess.csproj", "FitAppServer.DataAccess/"]
RUN dotnet restore "FitAppServerREST/FitAppServerREST.csproj"
COPY . .
WORKDIR "/src/FitAppServerREST"
RUN dotnet build "FitAppServerREST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitAppServerREST.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitAppServerREST.dll"]
