using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReaderController : ControllerBase
{
    private static List<Reader> _readers = new()
    {
        new Reader { Id = 1, Name = "Jordan Malik",  Email = "jmalik@gmail.com",    PhoneNumber = "403-555-0182", Address = "112 Elbow Dr SW, Calgary, AB",   MemberSince = new DateTime(2023, 2, 14) },
        new Reader { Id = 2, Name = "Priya Sharma",  Email = "priya.s@outlook.com", PhoneNumber = "587-555-0247", Address = "45 Crowchild Trail NW, Calgary", MemberSince = new DateTime(2023, 9, 1)  },
        new Reader { Id = 3, Name = "Tyler Nguyen",  Email = "tnguyen@email.com",   PhoneNumber = "403-555-0391", Address = "820 16 Ave NE, Calgary, AB",     MemberSince = new DateTime(2024, 1, 20) },
    };
    private static int _nextId = 4;

    [HttpGet]
    public IActionResult GetAll() => Ok(_readers);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var reader = _readers.FirstOrDefault(r => r.Id == id);
        if (reader == null)
            return NotFound(new { message = $"Reader {id} not found" });
        return Ok(reader);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Reader reader)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        reader.Id = _nextId++;
        reader.MemberSince = DateTime.Today;
        _readers.Add(reader);
        return CreatedAtAction(nameof(GetById), new { id = reader.Id }, reader);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Reader updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reader = _readers.FirstOrDefault(r => r.Id == id);
        if (reader == null)
            return NotFound();

        reader.Name        = updated.Name;
        reader.Email       = updated.Email;
        reader.PhoneNumber = updated.PhoneNumber;
        reader.Address     = updated.Address;
        return Ok(reader);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reader = _readers.FirstOrDefault(r => r.Id == id);
        if (reader == null) return NotFound();
        _readers.Remove(reader);
        return NoContent();
    }
}
