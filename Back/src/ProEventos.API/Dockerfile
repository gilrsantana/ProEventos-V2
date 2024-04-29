FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProEventos.API/ProEventos.API.csproj", "ProEventos.API/"]
COPY ["ProEventos.Application/ProEventos.Application.csproj", "ProEventos.Application/"]
COPY ["ProEventos.Domain/ProEventos.Domain.csproj", "ProEventos.Domain/"]
COPY ["ProEventos.Persistence/ProEventos.Persistence.csproj", "ProEventos.Persistence/"]
RUN dotnet restore "ProEventos.API/ProEventos.API.csproj"
COPY . .
WORKDIR "/src/ProEventos.API"
RUN dotnet build "ProEventos.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProEventos.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir Data
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]
