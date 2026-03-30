using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models;

public class Borrowing
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Book is required.")]
    [Display(Name = "Book")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Reader is required.")]
    [Display(Name = "Reader")]
    public int ReaderId { get; set; }

    // Navigation display (not stored, just for views)
    public string BookTitle { get; set; } = string.Empty;
    public string ReaderName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Borrow Date")]
    public DateTime BorrowDate { get; set; } = DateTime.Today;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Due Date")]
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);

    [DataType(DataType.Date)]
    [Display(Name = "Return Date")]
    public DateTime? ReturnDate { get; set; }

    [Display(Name = "Returned")]
    public bool IsReturned { get; set; } = false;

    [Display(Name = "Overdue Fee ($)")]
    [DataType(DataType.Currency)]
    public decimal OverdueFee { get; set; } = 0;

    public decimal CalculateOverdueFee()
    {
        var checkDate = ReturnDate ?? DateTime.Today;
        if (checkDate > DueDate)
        {
            var daysOverdue = (checkDate - DueDate).Days;
            return daysOverdue * 0.50m; // $0.50 per day
        }
        return 0;
    }
}
