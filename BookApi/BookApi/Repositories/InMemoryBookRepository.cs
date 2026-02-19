using BookApi.Models.Entities;
using BookApi.Repositories.Interfaces;

namespace BookApi.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public IReadOnlyList<Book> GetAllBooks()
    {
        return _books.AsReadOnly();
    }

    public Book AddBook(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public Book? GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public bool UpdateBook(Book book)
    {
        var index = _books.FindIndex(b => b.Id == book.Id);
        if(index == -1)
            return false;
        _books[index] = book;
        return true;
    }

    public bool DeleteBook(int id)
    {
        var existingBook = GetBookById(id);
        if (existingBook == null)
            return false;
        _books.Remove(existingBook);
        return true;
    }

    public bool IsIsbnAlreadyUsed(string isbn, int? excludingBookId = null)
    {
        return _books.Any(book => 
        string.Equals(book.ISBN, isbn, StringComparison.OrdinalIgnoreCase) && 
        (!excludingBookId.HasValue || book.Id != excludingBookId.Value)
        );
    }
}

