using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models;

public class Borrowing
{
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int ReaderId { get; set; }

    public DateTime BorrowDate { get; set; } = DateTime.Today;

    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; } = false;

    public decimal OverdueFee { get; set; } = 0;
}
