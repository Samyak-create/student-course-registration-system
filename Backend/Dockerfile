# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["StudentCourseRegistrationSystem.csproj", "./"]
RUN dotnet restore "StudentCourseRegistrationSystem.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/."
RUN dotnet build "StudentCourseRegistrationSystem.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "StudentCourseRegistrationSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentCourseRegistrationSystem.dll"]
