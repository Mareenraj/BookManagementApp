using BookApi.Mappings;
using BookApi.Models.DTOs.Requests;
using BookApi.Models.DTOs.Responses;
using BookApi.Repositories.Interfaces;
using BookApi.Services.Interfaces;

namespace BookApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<BookResponseDto> GetAllBooks()
    {
        return _repository.GetAllBooks().Select(book => book.ToResponseDto()).ToList();
    }

    public (bool ok, string? error, BookResponseDto? created) AddBook(CreateBookDto dto)
    {
        if (_repository.IsIsbnAlreadyUsed(dto.ISBN))
        {
            return (false, "A book with the same ISBN already exists.", null);
        }
        if (dto.PublicationDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return (false, "Publication date cannot be in the future.", null);
        }
        var book = _repository.AddBook(dto.ToEntity());
        return (true, null, book.ToResponseDto());
    }

    public BookResponseDto? GetBookById(int id)
    {
        return _repository.GetBookById(id)?.ToResponseDto();
    }

    public (bool ok, string? error) UpdateBook(int id, UpdateBookDto dto)
    {
        if (_repository.GetBookById(id) == null)
        {
            return (false, "Book not found.");
        }
        if (_repository.IsIsbnAlreadyUsed(dto.ISBN, id))
        {
            return (false, "A book with the same ISBN already exists.");
        }
        if (dto.PublicationDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return (false, "Publication date cannot be in the future.");
        }

        var result = _repository.UpdateBook(dto.ToEntity(id));

        return result ? (true, null) : (false, "Update failed.");
    }

    public bool DeleteBook(int id)
    {
        return _repository.DeleteBook(id);
    }
}
