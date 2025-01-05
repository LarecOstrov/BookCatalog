using BookCatalogBackend.Context;
using BookCatalog.Shared.DbModels;
using BookCatalogBackend.Models;
using BookCatalogBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookCatalog.Shared.Models;

namespace BookCatalogBackend.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Book>> GetBooksAsync(string? title, string? author, string? genre, int page = 1, int pageSize = 10, string sorting = "createat", string order = "desc")
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title)) query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));
            if (!string.IsNullOrEmpty(author)) query = query.Where(b => b.Author.ToLower().Contains(author.ToLower()));
            if (!string.IsNullOrEmpty(genre)) query = query.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));

            var total = await query.CountAsync();
            query = sorting switch
            {
                "createat" when order == "asc" => query.OrderBy(b => b.CreatedAt),
                "createat" when order == "desc" => query.OrderByDescending(b => b.CreatedAt),
                "title" when order == "asc" => query.OrderBy(b => b.Title),
                "title" when order == "desc" => query.OrderByDescending(b => b.Title),
                "author" when order == "asc" => query.OrderBy(b => b.Author),
                "author" when order == "desc" => query.OrderByDescending(b => b.Author),
                "genre" when order == "asc" => query.OrderBy(b => b.Genre),
                "genre" when order == "desc" => query.OrderByDescending(b => b.Genre),
                "publishedyear" when order == "asc" => query.OrderBy(b => b.PublishedYear),
                "publishedyear" when order == "desc" => query.OrderByDescending(b => b.PublishedYear),
                _ => query.OrderBy(b => b.CreatedAt)
            };
            
            var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Book> { Total = total, Data = books };
        }

        public async Task<Book?> GetBookAsync(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateBookAsync(Guid id, Book book)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null) return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.PublishedYear = book.PublishedYear;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ServiceResult> BulkUploadAsync(IFormFile file)
        {
            if (!file.FileName.Contains(".csv")) return new ServiceResult { Success = false, Message = "Invalid file format." };

            using var reader = new StreamReader(file.OpenReadStream());
            var books = new List<Book>();
            string? line;
            var lineNumber = 0;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    try
                    {
                        books.Add(new Book
                        {
                            Title = parts[0],
                            Author = parts[1],
                            Genre = parts[2],
                            PublishedYear = int.Parse(parts[3])
                        });
                        lineNumber++;
                    }
                    catch (Exception ex)
                    {
                        if (lineNumber != 0 && (ex is FormatException || ex is OverflowException))
                        {
                            return new ServiceResult { Success = false, Message = $"Invalid data format on line {lineNumber}: {line}." };
                        }
                    }
                }
            }

            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
            return new ServiceResult { Success = true, Message = $"Added {lineNumber} books" };
        }
    }
}
