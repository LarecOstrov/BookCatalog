using BookCatalog.Shared.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogBackend.Context
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books => Set<Book>();
    }
}
