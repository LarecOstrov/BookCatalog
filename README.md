# Book Catalog Application

This repository contains the Book Catalog application, a two-tier application with a frontend and a backend. The frontend is implemented using Blazor WebAssembly, and the backend is an ASP.NET Core Web API.

## Prerequisites

To run this application locally, ensure you have the following installed:

- [Docker](https://www.docker.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the following workloads:
  - ASP.NET and web development
  - Container development tools
- .NET SDK (version 6.0 or higher)

---

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/LarecOstrov/BookCatalog.git
   cd <repository-folder>
   ```

## Running Locally Without Containers
   - Open solution in Visual Studio.
   - Change var backendApiUrl to "https://localhost:4568" in Program.cs in BookCatalogFrontend project
   - Set the startup project to `BookCatalogBackend`.
   - Right-click the solution > Set Startup Projects.
   - Select Multiple startup projects.
   - Set each project BookCatalogBackend and BookCatalogFrontend to Start.
   - Run Multiple project
     
**Verify Application**:
   - Open `https://localhost:44365` in your browser to view the application.
---

## Running with Docker Compose

Ensure that var backendApiUrl is "http://localhost:4567" in Program.cs in BookCatalogFrontend project

**Build and Start Containers**:
   
   From the root of the repository, run the following command:
   ```bash
   docker-compose up --build
   ```

   - This will build and start the `backend` and `frontend` services.
   - The backend will be accessible at `http://localhost:4567`.
   - The frontend will be accessible at `http://localhost:5199`.

**Verify Application**:
   Open `http://localhost:5199` in your browser to view the application.

---

## Troubleshooting

1. **Application Not Starting**:
   - Ensure Docker is running and Visual Studio is set up correctly.

2. **Frontend Cannot Connect to Backend**:
   - Verify that the backendApiUrl variable in Program.cs is  correctly configured.
   - Check network settings if running in Docker Compose.

3. **Port Conflicts**:
   - Check if the ports `4567`, `5199`, or `4568` are already in use and update the configuration if necessary.

---

## License

This project is licensed under the [MIT License](LICENSE).

