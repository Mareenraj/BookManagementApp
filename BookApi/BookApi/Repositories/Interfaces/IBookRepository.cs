using BookApi.Models.Entities;

namespace BookApi.Repositories.Interfaces;

public interface IBookRepository
{
    IReadOnlyList<Book> GetAllBooks();

    Book? GetBookById(int id);

    Book AddBook(Book book);

    bool UpdateBook(Book book);

    bool DeleteBook(int id);

    bool IsIsbnAlreadyUsed(string isbn, int? excludingBookId = null);
}

