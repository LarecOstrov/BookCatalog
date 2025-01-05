using BookCatalog.Shared.DbModels;
using BookCatalogBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? genre,
        [FromQuery] string sorting ="careteat", [FromQuery] string order = "desc", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _bookService.GetBooksAsync(title, author, genre, page, pageSize, sorting, order);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var result = await _bookService.GetBookAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        var createdBook = await _bookService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBooks), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
    {
        var updated = await _bookService.UpdateBookAsync(id, book);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPost("bulk-upload")]
    public async Task<IActionResult> BulkUpload([FromForm] IFormFile file)
    {
        var result = await _bookService.BulkUploadAsync(file);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }
}