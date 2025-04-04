# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventsWepApp.API/EventsWepApp.API.csproj", "EventsWepApp.API/"]
COPY ["EventsWebApp.Application/EventsWebApp.Application.csproj", "EventsWebApp.Application/"]
COPY ["EventsWebApp.Domain/EventsWebApp.Domain.csproj", "EventsWebApp.Domain/"]
COPY ["EventsWebApp.Shared/EventsWebApp.Shared.csproj", "EventsWebApp.Shared/"]
COPY ["EventsWebApp.Infrastructure/EventsWebApp.Infrastructure.csproj", "EventsWebApp.Infrastructure/"]
RUN dotnet restore "./EventsWepApp.API/EventsWepApp.API.csproj"
COPY . .
WORKDIR "/src/EventsWepApp.API"
RUN dotnet build "./EventsWepApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventsWepApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY EventsWepApp.API/EventWebApp.db .
USER root
RUN chmod 666 /app/EventWebApp.db
USER $APP_UID
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "EventsWepApp.API.dll"]