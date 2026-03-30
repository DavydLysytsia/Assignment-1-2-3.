using Assignment3.Models;

namespace Assignment3.Repositories;

public class BorrowingRepository : IBorrowingRepository
{
    private static readonly List<Borrowing> _borrowings = new()
    {
        new Borrowing { Id = 1, BookId = 2, ReaderId = 1, BorrowDate = new DateTime(2025, 1, 10), DueDate = new DateTime(2025, 1, 24), IsReturned = false },
        new Borrowing { Id = 2, BookId = 1, ReaderId = 3, BorrowDate = new DateTime(2025, 2,  1), DueDate = new DateTime(2025, 2, 15), ReturnDate = new DateTime(2025, 2, 12), IsReturned = true },
    };

    private static int _nextId = 3;

    public IEnumerable<Borrowing> GetAll() => _borrowings.ToList();

    public Borrowing? GetById(int id) => _borrowings.FirstOrDefault(b => b.Id == id);

    public Borrowing Create(Borrowing borrowing)
    {
        borrowing.Id = _nextId++;
        borrowing.OverdueFee = borrowing.CalculateOverdueFee();
        _borrowings.Add(borrowing);
        return borrowing;
    }

    public Borrowing? Update(Borrowing borrowing)
    {
        var existing = GetById(borrowing.Id);
        if (existing == null) return null;

        existing.BookId     = borrowing.BookId;
        existing.ReaderId   = borrowing.ReaderId;
        existing.BorrowDate = borrowing.BorrowDate;
        existing.DueDate    = borrowing.DueDate;
        existing.ReturnDate = borrowing.ReturnDate;
        existing.IsReturned = borrowing.IsReturned;
        existing.OverdueFee = borrowing.CalculateOverdueFee();
        return existing;
    }

    public bool Delete(int id)
    {
        var b = GetById(id);
        if (b == null) return false;
        _borrowings.Remove(b);
        return true;
    }
}
