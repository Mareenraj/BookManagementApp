using BookApi.Models.DTOs.Requests;
using BookApi.Models.DTOs.Responses;
using BookApi.Models.Entities;

namespace BookApi.Mappings;

public static class BookMapping
{
    public static Book ToEntity(this CreateBookDto createBookDto) => new()
    {
        Title = createBookDto.Title.Trim(),
        Author = createBookDto.Author.Trim(),
        ISBN = createBookDto.ISBN.Trim(),
        PublicationDate = createBookDto.PublicationDate
    };

    public static Book ToEntity(this UpdateBookDto updateBookDto, int id) => new()
    {
        Id = id,
        Title = updateBookDto.Title.Trim(),
        Author = updateBookDto.Author.Trim(),
        ISBN = updateBookDto.ISBN,
        PublicationDate = updateBookDto.PublicationDate
    };

    public static BookResponseDto ToResponseDto(this Book book) =>
        new(book.Id, book.Title, book.Author, book.ISBN, book.PublicationDate);
}
