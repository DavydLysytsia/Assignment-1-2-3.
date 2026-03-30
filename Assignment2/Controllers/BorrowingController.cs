using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowingController : ControllerBase
{
    private static List<Borrowing> _borrowings = new()
    {
        new Borrowing { Id = 1, BookId = 2, ReaderId = 1, BorrowDate = new DateTime(2025, 1, 10), DueDate = new DateTime(2025, 1, 24), IsReturned = false },
        new Borrowing { Id = 2, BookId = 1, ReaderId = 3, BorrowDate = new DateTime(2025, 2, 1),  DueDate = new DateTime(2025, 2, 15), ReturnDate = new DateTime(2025, 2, 12), IsReturned = true },
    };
    private static int _nextId = 3;

    [HttpGet]
    public IActionResult GetAll() => Ok(_borrowings);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var b = _borrowings.FirstOrDefault(b => b.Id == id);
        return b == null ? NotFound() : Ok(b);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Borrowing borrowing)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        borrowing.Id = _nextId++;
        borrowing.BorrowDate = DateTime.Today;
        borrowing.DueDate    = DateTime.Today.AddDays(14);
        borrowing.IsReturned = false;

        if (DateTime.Today > borrowing.DueDate)
            borrowing.OverdueFee = (decimal)(DateTime.Today - borrowing.DueDate).TotalDays * 0.50m;

        _borrowings.Add(borrowing);
        return CreatedAtAction(nameof(GetById), new { id = borrowing.Id }, borrowing);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Borrowing updated)
    {
        var b = _borrowings.FirstOrDefault(b => b.Id == id);
        if (b == null) return NotFound();

        b.DueDate    = updated.DueDate;
        b.ReturnDate = updated.ReturnDate;
        b.IsReturned = updated.IsReturned;

        if (b.IsReturned && b.ReturnDate.HasValue && b.ReturnDate > b.DueDate)
        {
            var daysLate = (b.ReturnDate.Value - b.DueDate).Days;
            b.OverdueFee = daysLate * 0.50m;
        }

        return Ok(b);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var b = _borrowings.FirstOrDefault(b => b.Id == id);
        if (b == null) return NotFound();
        _borrowings.Remove(b);
        return NoContent();
    }
}
