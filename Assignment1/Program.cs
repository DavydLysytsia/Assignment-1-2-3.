using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var books = new List<Book>
{
    new Book { Id = 1, Title = "The Hobbit",                              Author = "J.R.R. Tolkien",   ISBN = "978-0547928227", Genre = "Fantasy",    Year = 1937, IsAvailable = true  },
    new Book { Id = 2, Title = "Harry Potter and the Philosophers Stone", Author = "J.K. Rowling",     ISBN = "978-0439708180", Genre = "Fantasy",    Year = 1997, IsAvailable = false },
    new Book { Id = 3, Title = "Clean Code",                              Author = "Robert C. Martin", ISBN = "978-0132350884", Genre = "Technology", Year = 2008, IsAvailable = true  },
    new Book { Id = 4, Title = "The Pragmatic Programmer",                Author = "Andy Hunt",        ISBN = "978-0201616224", Genre = "Technology", Year = 1999, IsAvailable = true  },
};
int nextBookId = 5;

app.MapGet("/api/books", () =>
{
    return Results.Ok(books);
});

app.MapGet("/api/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null)
        return Results.NotFound(new { message = $"No book found with id {id}" });

    return Results.Ok(book);
});

app.MapPost("/api/books", (Book book) =>
{
    book.Id = nextBookId++;
    book.IsAvailable = true;
    books.Add(book);
    return Results.Created($"/api/books/{book.Id}", book);
});

app.MapPut("/api/books/{id}", (int id, Book updated) =>
{
    var existing = books.FirstOrDefault(b => b.Id == id);
    if (existing == null)
        return Results.NotFound(new { message = $"Book {id} not found" });

    existing.Title       = updated.Title;
    existing.Author      = updated.Author;
    existing.ISBN        = updated.ISBN;
    existing.Genre       = updated.Genre;
    existing.Year        = updated.Year;
    existing.IsAvailable = updated.IsAvailable;

    return Results.Ok(existing);
});

app.MapDelete("/api/books/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);
    if (book == null)
        return Results.NotFound(new { message = $"Book {id} not found" });

    books.Remove(book);
    return Results.Ok(new { message = $"Deleted '{book.Title}'" });
});


var readers = new List<Reader>
{
    new Reader { Id = 1, Name = "Jordan Malik",  Email = "jmalik@gmail.com",    Phone = "403-555-0182", MemberSince = "2023-02-14" },
    new Reader { Id = 2, Name = "Priya Sharma",  Email = "priya.s@outlook.com", Phone = "587-555-0247", MemberSince = "2023-09-01" },
    new Reader { Id = 3, Name = "Tyler Nguyen",  Email = "tnguyen@email.com",   Phone = "403-555-0391", MemberSince = "2024-01-20" },
};
int nextReaderId = 4;

app.MapGet("/api/readers", () => Results.Ok(readers));

app.MapGet("/api/readers/{id}", (int id) =>
{
    var r = readers.FirstOrDefault(r => r.Id == id);
    return r is null ? Results.NotFound(new { message = $"Reader {id} not found" }) : Results.Ok(r);
});

app.MapPost("/api/readers", (Reader reader) =>
{
    reader.Id = nextReaderId++;
    readers.Add(reader);
    return Results.Created($"/api/readers/{reader.Id}", reader);
});

app.MapPut("/api/readers/{id}", (int id, Reader updated) =>
{
    var r = readers.FirstOrDefault(r => r.Id == id);
    if (r == null) return Results.NotFound();

    r.Name        = updated.Name;
    r.Email       = updated.Email;
    r.Phone       = updated.Phone;
    r.MemberSince = updated.MemberSince;
    return Results.Ok(r);
});

app.MapDelete("/api/readers/{id}", (int id) =>
{
    var r = readers.FirstOrDefault(r => r.Id == id);
    if (r == null) return Results.NotFound();
    readers.Remove(r);
    return Results.NoContent();
});


var borrowings = new List<Borrowing>
{
    new Borrowing { Id = 1, BookId = 2, ReaderId = 1, BorrowDate = "2025-01-10", DueDate = "2025-01-24", ReturnDate = null,         IsReturned = false },
    new Borrowing { Id = 2, BookId = 1, ReaderId = 3, BorrowDate = "2025-02-01", DueDate = "2025-02-15", ReturnDate = "2025-02-12", IsReturned = true  },
};
int nextBorrowingId = 3;

app.MapGet("/api/borrowings", () => Results.Ok(borrowings));

app.MapGet("/api/borrowings/{id}", (int id) =>
{
    var b = borrowings.FirstOrDefault(b => b.Id == id);
    return b is null ? Results.NotFound() : Results.Ok(b);
});

app.MapPost("/api/borrowings", (Borrowing b) =>
{
    b.Id = nextBorrowingId++;
    b.IsReturned = false;
    borrowings.Add(b);

    var book = books.FirstOrDefault(bk => bk.Id == b.BookId);
    if (book != null) book.IsAvailable = false;

    return Results.Created($"/api/borrowings/{b.Id}", b);
});

app.MapPut("/api/borrowings/{id}", (int id, Borrowing updated) =>
{
    var b = borrowings.FirstOrDefault(b => b.Id == id);
    if (b == null) return Results.NotFound();

    b.DueDate    = updated.DueDate;
    b.ReturnDate = updated.ReturnDate;
    b.IsReturned = updated.IsReturned;

    if (b.IsReturned)
    {
        var book = books.FirstOrDefault(bk => bk.Id == b.BookId);
        if (book != null) book.IsAvailable = true;
    }

    return Results.Ok(b);
});

app.MapDelete("/api/borrowings/{id}", (int id) =>
{
    var b = borrowings.FirstOrDefault(b => b.Id == id);
    if (b == null) return Results.NotFound();
    borrowings.Remove(b);
    return Results.NoContent();
});

app.Run();

class Book
{
    public int    Id          { get; set; }
    public string Title       { get; set; } = "";
    public string Author      { get; set; } = "";
    public string ISBN        { get; set; } = "";
    public string Genre       { get; set; } = "";
    public int    Year        { get; set; }
    public bool   IsAvailable { get; set; } = true;
}

class Reader
{
    public int    Id          { get; set; }
    public string Name        { get; set; } = "";
    public string Email       { get; set; } = "";
    public string Phone       { get; set; } = "";
    public string MemberSince { get; set; } = "";
}

class Borrowing
{
    public int     Id         { get; set; }
    public int     BookId     { get; set; }
    public int     ReaderId   { get; set; }
    public string  BorrowDate { get; set; } = "";
    public string  DueDate    { get; set; } = "";
    public string? ReturnDate { get; set; }
    public bool    IsReturned { get; set; } = false;
}
