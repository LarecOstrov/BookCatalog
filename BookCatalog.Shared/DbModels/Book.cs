using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Shared.DbModels
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsEditing { get; set; } = false;
    }
}
