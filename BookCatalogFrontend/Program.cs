using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookCatalogFrontend;
using BookCatalogFrontend.Services.Interfaces;
using BookCatalogFrontend.Services.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:4567/") });
builder.Services.AddScoped<IBookHttpService, BookHttpService>();

await builder.Build().RunAsync();
