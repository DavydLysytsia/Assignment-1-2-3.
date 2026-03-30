using Assignment3.Models;

namespace Assignment3.Repositories;

public interface IReaderRepository
{
    IEnumerable<Reader> GetAll();
    Reader? GetById(int id);
    Reader Create(Reader reader);
    Reader? Update(Reader reader);
    bool Delete(int id);
}
