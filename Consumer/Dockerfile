FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only csproj
COPY ./Consumer.csproj ./ 

# Restore dependencies
RUN dotnet restore "./Consumer.csproj"

# Copy the rest of the app
COPY . .

# Set working directory to the app folder (where Program.cs is)
WORKDIR /src

# Build the project
RUN dotnet build "Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.dll"]
