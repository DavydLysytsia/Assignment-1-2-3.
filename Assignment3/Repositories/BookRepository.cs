using Assignment3.Models;

namespace Assignment3.Repositories;

public class BookRepository : IBookRepository
{
    private static readonly List<Book> _books = new()
    {
        new Book { Id = 1, Title = "The Hobbit",                 Author = "J.R.R. Tolkien",   ISBN = "978-0547928227", Genre = "Fantasy",    PublicationYear = 1937, IsAvailable = true  },
        new Book { Id = 2, Title = "Harry Potter",               Author = "J.K. Rowling",     ISBN = "978-0439708180", Genre = "Fantasy",    PublicationYear = 1997, IsAvailable = false },
        new Book { Id = 3, Title = "Clean Code",                 Author = "Robert C. Martin", ISBN = "978-0132350884", Genre = "Technology", PublicationYear = 2008, IsAvailable = true  },
        new Book { Id = 4, Title = "The Pragmatic Programmer",   Author = "Andy Hunt",        ISBN = "978-0201616224", Genre = "Technology", PublicationYear = 1999, IsAvailable = true  },
        new Book { Id = 5, Title = "Atomic Habits",              Author = "James Clear",      ISBN = "978-0735211292", Genre = "Self-Help",  PublicationYear = 2018, IsAvailable = true  },
    };

    private static int _nextId = 6;

    public IEnumerable<Book> GetAll() => _books.ToList();

    public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public Book Create(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public Book? Update(Book book)
    {
        var existing = GetById(book.Id);
        if (existing == null) return null;

        existing.Title           = book.Title;
        existing.Author          = book.Author;
        existing.ISBN            = book.ISBN;
        existing.Genre           = book.Genre;
        existing.PublicationYear = book.PublicationYear;
        existing.IsAvailable     = book.IsAvailable;
        return existing;
    }

    public bool Delete(int id)
    {
        var book = GetById(id);
        if (book == null) return false;
        _books.Remove(book);
        return true;
    }
}
