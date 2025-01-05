using BookCatalogFrontend.Services.Interfaces;
using BookCatalog.Shared.DbModels;
using System.Net.Http.Json;
using BookCatalog.Shared.Models;
using Microsoft.AspNetCore.Http;
using BookCatalogBackend.Models;

namespace BookCatalogFrontend.Services.Implementations
{
    public class BookHttpService : IBookHttpService
    {
        private readonly HttpClient _httpClient;

        public BookHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<PagedResult<Book>> GetBooksAsync(string? title, string? author, string? genre, string? sorting, string? order, int? page = 0, int? pageSize = 20)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResult<Book>>($"api/book?title={title}&author={author}&genre={genre}&sorting={sorting}&order={order}&page={page}&pageSize={pageSize}");
            return response ?? new PagedResult<Book>();
        }

        public async Task<Book?> GetBookAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"api/book/{id}");
        }

        public async Task CreateBookAsync(Book book)
        {
            await _httpClient.PostAsJsonAsync("api/book", book);
        }

        public async Task UpdateBookAsync(Guid id, Book book)
        {
            await _httpClient.PutAsJsonAsync($"api/book/{id}", book);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/book/{id}");
        }

        public async Task<ServiceResult> BulkUploadAsync(MultipartFormDataContent content)
        {            
            var response = await _httpClient.PostAsync("api/book/bulk-upload", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return new ServiceResult { Success = response.IsSuccessStatusCode, Message = responseContent };
            
        }
    }
}
