using Assignment3.Models;

namespace Assignment3.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Book Create(Book book);
    Book? Update(Book book);
    bool Delete(int id);
}
