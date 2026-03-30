using Assignment3.Models;

namespace Assignment3.Repositories;

public interface IBorrowingRepository
{
    IEnumerable<Borrowing> GetAll();
    Borrowing? GetById(int id);
    Borrowing Create(Borrowing borrowing);
    Borrowing? Update(Borrowing borrowing);
    bool Delete(int id);
}
