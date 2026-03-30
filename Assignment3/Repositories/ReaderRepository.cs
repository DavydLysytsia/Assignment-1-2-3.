using Assignment3.Models;

namespace Assignment3.Repositories;

public class ReaderRepository : IReaderRepository
{
    private static readonly List<Reader> _readers = new()
    {
        new Reader { Id = 1, Name = "Jordan Malik",  Email = "jmalik@gmail.com",    PhoneNumber = "403-555-0182", Address = "112 Elbow Dr SW, Calgary, AB",   MemberSince = new DateTime(2023, 2, 14) },
        new Reader { Id = 2, Name = "Priya Sharma",  Email = "priya.s@outlook.com", PhoneNumber = "587-555-0247", Address = "45 Crowchild Trail NW, Calgary", MemberSince = new DateTime(2023, 9, 1)  },
        new Reader { Id = 3, Name = "Tyler Nguyen",  Email = "tnguyen@email.com",   PhoneNumber = "403-555-0391", Address = "820 16 Ave NE, Calgary, AB",     MemberSince = new DateTime(2024, 1, 20) },
        new Reader { Id = 4, Name = "Fatima Hassan", Email = "fhassan@email.com",   PhoneNumber = "587-555-0512", Address = "330 Centre St N, Calgary, AB",   MemberSince = new DateTime(2024, 3, 5)  },
    };

    private static int _nextId = 5;

    public IEnumerable<Reader> GetAll() => _readers.ToList();

    public Reader? GetById(int id) => _readers.FirstOrDefault(r => r.Id == id);

    public Reader Create(Reader reader)
    {
        reader.Id = _nextId++;
        _readers.Add(reader);
        return reader;
    }

    public Reader? Update(Reader reader)
    {
        var existing = GetById(reader.Id);
        if (existing == null) return null;

        existing.Name        = reader.Name;
        existing.Email       = reader.Email;
        existing.PhoneNumber = reader.PhoneNumber;
        existing.Address     = reader.Address;
        existing.MemberSince = reader.MemberSince;
        return existing;
    }

    public bool Delete(int id)
    {
        var reader = GetById(id);
        if (reader == null) return false;
        _readers.Remove(reader);
        return true;
    }
}
