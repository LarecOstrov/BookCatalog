using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookCatalogFrontend;
using BookCatalogFrontend.Services.Interfaces;
using BookCatalogFrontend.Services.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//var backendApiUrl = "https://localhost:4568";
var backendApiUrl = "http://localhost:4567"; //docker
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendApiUrl) });

builder.Services.AddScoped<IBookHttpService, BookHttpService>();

await builder.Build().RunAsync();
