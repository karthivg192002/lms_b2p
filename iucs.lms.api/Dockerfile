# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

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
COPY ["iucs.lms.api/iucs.lms.api.csproj", "iucs.lms.api/"]
COPY ["iucs.lms.application/iucs.lms.application.csproj", "iucs.lms.application/"]
COPY ["iucs.lms.domain/iucs.lms.domain.csproj", "iucs.lms.domain/"]
RUN dotnet restore "./iucs.lms.api/iucs.lms.api.csproj"
COPY . .
WORKDIR "/src/iucs.lms.api"
RUN dotnet build "./iucs.lms.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./iucs.lms.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iucs.lms.api.dll"]