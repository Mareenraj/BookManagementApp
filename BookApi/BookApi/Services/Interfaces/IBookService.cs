using BookApi.Models.DTOs.Requests;
using BookApi.Models.DTOs.Responses;

namespace BookApi.Services.Interfaces
{
    public interface IBookService
    {
        IReadOnlyList<BookResponseDto> GetAllBooks();

        BookResponseDto? GetBookById(int id);

        (bool ok, string? error, BookResponseDto? created) AddBook(CreateBookDto dto);
        (bool ok, string? error) UpdateBook(int id, UpdateBookDto dto);
         bool DeleteBook(int id);
    }
}
