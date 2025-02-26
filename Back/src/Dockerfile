# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProEventos.API/ProEventos.API.csproj", "ProEventos.API/"]
COPY ["ProEventos.Application/ProEventos.Application.csproj", "ProEventos.Application/"]
COPY ["ProEventos.Domain/ProEventos.Domain.csproj", "ProEventos.Domain/"]
COPY ["ProEventos.Persistence/ProEventos.Persistence.csproj", "ProEventos.Persistence/"]
RUN dotnet restore "./ProEventos.API/ProEventos.API.csproj"
COPY . .
WORKDIR "/src/ProEventos.API"
RUN dotnet build "./ProEventos.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProEventos.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]
# docker run -v "C:\Configuracoes:/app/dados" --name ProEventosApi-1.02 -p 8080:8080 -p 8081:8081 gilrsantana/proeventosapi:v1.02