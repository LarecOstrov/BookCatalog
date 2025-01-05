using BookCatalog.Shared.DbModels;
using BookCatalog.Shared.Models;
using BookCatalogBackend.Models;
using Microsoft.AspNetCore.Http;

namespace BookCatalogFrontend.Services.Interfaces
{
    public interface IBookHttpService
    {
        Task<PagedResult<Book>> GetBooksAsync(string? title, string? author, string? genre, string? sorting, string? order, int? page = 0, int? pageSize = 20);
        Task<Book?> GetBookAsync(Guid id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Guid id, Book book);
        Task DeleteBookAsync(Guid id);
        Task<ServiceResult> BulkUploadAsync(MultipartFormDataContent content);
    }
}
