# Use the official .NET 9 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["ILS_BE.csproj", "./"]
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 9 runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set environment variables (override in docker-compose or deployment)
# ENV ASPNETCORE_ENVIRONMENT=Production
# ENV ConnectionStrings__PostgresConnection=Host=yourdb;Database=yourdb;Username=youruser;Password=yourpass

# Optionally run migrations on startup (uncomment if needed)
# ENTRYPOINT ["dotnet", "ILS_BE.dll", "migrate", "--no-build"]

# Start the application
ENTRYPOINT ["dotnet", "ILS_BE.dll"]
