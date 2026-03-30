using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    // using static list so data stays between requests
    private static List<Book> _books = new()
    {
        new Book { Id = 1, Title = "The Hobbit",             Author = "J.R.R. Tolkien",   ISBN = "978-0547928227", Genre = "Fantasy",    PublicationYear = 1937, IsAvailable = true  },
        new Book { Id = 2, Title = "Harry Potter",           Author = "J.K. Rowling",     ISBN = "978-0439708180", Genre = "Fantasy",    PublicationYear = 1997, IsAvailable = false },
        new Book { Id = 3, Title = "Clean Code",             Author = "Robert C. Martin", ISBN = "978-0132350884", Genre = "Technology", PublicationYear = 2008, IsAvailable = true  },
        new Book { Id = 4, Title = "The Pragmatic Programmer", Author = "Andy Hunt",      ISBN = "978-0201616224", Genre = "Technology", PublicationYear = 1999, IsAvailable = true  },
    };
    private static int _nextId = 5;

    // GET: api/book
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_books);
    }

    // GET: api/book/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new { message = $"Book with id {id} was not found" });

        return Ok(book);
    }

    // POST: api/book
    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        book.Id = _nextId++;
        book.IsAvailable = true;
        _books.Add(book);

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // PUT: api/book/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = _books.FirstOrDefault(b => b.Id == id);
        if (existing == null)
            return NotFound(new { message = $"Book {id} not found" });

        existing.Title          = updated.Title;
        existing.Author         = updated.Author;
        existing.ISBN           = updated.ISBN;
        existing.Genre          = updated.Genre;
        existing.PublicationYear = updated.PublicationYear;
        existing.IsAvailable    = updated.IsAvailable;

        return Ok(existing);
    }

    // DELETE: api/book/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound(new { message = $"Book {id} not found" });

        _books.Remove(book);
        return Ok(new { message = $"'{book.Title}' has been deleted" });
    }
}
