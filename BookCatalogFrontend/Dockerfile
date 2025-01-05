# Use a lightweight Nginx image to serve the static files
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY ["BookCatalogFrontend/BookCatalogFrontend.csproj", "BookCatalogFrontend/"]
RUN dotnet restore "BookCatalogFrontend/BookCatalogFrontend.csproj"

# Build and publish the app
COPY . .
WORKDIR "/app/BookCatalogFrontend"
RUN dotnet publish "BookCatalogFrontend.csproj" -c Release -o /app/dist

# Use a lightweight image to serve the app with Nginx
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html
COPY --from=build /app/dist/wwwroot .
ENTRYPOINT ["nginx", "-g", "daemon off;"]