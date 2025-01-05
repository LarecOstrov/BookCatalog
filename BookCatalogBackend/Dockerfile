# Base stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BookCatalogBackend/BookCatalogBackend.csproj", "BookCatalogBackend/"]
COPY ["BookCatalog.Shared/BookCatalog.Shared.csproj", "BookCatalog.Shared/"]
RUN dotnet restore "BookCatalogBackend/BookCatalogBackend.csproj"
COPY . .
WORKDIR "/src/BookCatalogBackend"
RUN dotnet build "BookCatalogBackend.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BookCatalogBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookCatalogBackend.dll"]
