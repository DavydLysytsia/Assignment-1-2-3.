using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models;

public class Reader
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    public DateTime MemberSince { get; set; } = DateTime.Today;
}
