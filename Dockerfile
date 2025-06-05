# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy solution file
COPY *.sln ./

# Copy all project files (for restore to succeed)
COPY Application/*.csproj ./Application/
COPY CodeshareAPI/*.csproj ./CodeshareAPI/
COPY Dependency/*.csproj ./Dependency/
COPY DomainLayer/*.csproj ./DomainLayer/
COPY Infrastructure/*.csproj ./Infrastructure/

# Optional: Disable fallback NuGet path (fixes Linux container issue)
ENV NUGET_FALLBACK_PACKAGES=""

# Restore all dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Set working directory to main project
WORKDIR /app/CodeshareAPI

# Publish the project
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/CodeshareAPI/out .

# Run the app
ENTRYPOINT ["dotnet", "CodeshareAPI.dll"]
