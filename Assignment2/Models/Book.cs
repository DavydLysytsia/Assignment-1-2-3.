using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author name is required")]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string ISBN { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    [Range(1000, 2100, ErrorMessage = "Please enter a valid year")]
    public int PublicationYear { get; set; }

    public bool IsAvailable { get; set; } = true;
}
