using BookCatalogBackend.Context;
using BookCatalogBackend.Repositories.Implementations;
using BookCatalogBackend.Repositories.Interfaces;
using BookCatalogBackend.Services.Implementations;
using BookCatalogBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<BookDbContext>(options => options.UseInMemoryDatabase("BookCatalog"));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition =>
                new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromSeconds(1), 
                    SegmentsPerWindow = 5,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0 
                }));

    options.RejectionStatusCode = 429;

    options.OnRejected = (context, token) =>
    {
        Console.WriteLine($"Rate limit exceeded: IP {context.HttpContext.Connection.RemoteIpAddress}, Requests: {context.HttpContext.Request.Path}");
        return ValueTask.CompletedTask;
    };
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
app.UseCors();
app.MapFallbackToController("Index", "Home");
app.Run();


