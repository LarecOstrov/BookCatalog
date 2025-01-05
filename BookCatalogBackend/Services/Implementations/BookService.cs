using BookCatalog.Shared.DbModels;
using BookCatalog.Shared.Models;
using BookCatalogBackend.Models;
using BookCatalogBackend.Repositories.Interfaces;
using BookCatalogBackend.Services.Interfaces;

namespace BookCatalogBackend.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Book>> GetBooksAsync(string? title, string? author, string? genre, int page, int pageSize, string sorting, string order)
        {
            return await _repository.GetBooksAsync(title, author, genre, page, pageSize, sorting, order);
        }

        public async Task<Book?> GetBookAsync(Guid id)
        {
            return await _repository.GetBookAsync(id);
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            return await _repository.CreateBookAsync(book);
        }

        public async Task<bool> UpdateBookAsync(Guid id, Book book)
        {
            return await _repository.UpdateBookAsync(id, book);
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            return await _repository.DeleteBookAsync(id);
        }

        public async Task<ServiceResult> BulkUploadAsync(IFormFile file)
        {
            return await _repository.BulkUploadAsync(file);
        }
    }
}

