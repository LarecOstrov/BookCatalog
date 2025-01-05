using BookCatalog.Shared.DbModels;
using BookCatalog.Shared.Models;
using BookCatalogBackend.Models;

namespace BookCatalogBackend.Services.Interfaces
{
    public interface IBookService
    {
        Task<PagedResult<Book>> GetBooksAsync(string? title, string? author, string? genre, int page, int pageSize, string sorting, string order);
        Task<Book?> GetBookAsync(Guid id);
        Task<Book> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Guid id, Book book);
        Task<bool> DeleteBookAsync(Guid id);
        Task<ServiceResult> BulkUploadAsync(IFormFile file);
    }
}
