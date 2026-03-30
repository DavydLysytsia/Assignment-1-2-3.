using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required.")]
    [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters.")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required.")]
    [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters.")]
    public string ISBN { get; set; } = string.Empty;

    [StringLength(50)]
    public string Genre { get; set; } = string.Empty;

    [Range(1000, 2100, ErrorMessage = "Publication year must be between 1000 and 2100.")]
    [Display(Name = "Publication Year")]
    public int PublicationYear { get; set; }

    [Display(Name = "Available")]
    public bool IsAvailable { get; set; } = true;
}
