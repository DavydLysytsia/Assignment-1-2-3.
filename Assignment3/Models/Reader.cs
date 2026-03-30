using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models;

public class Reader
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [StringLength(20)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(300)]
    public string Address { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Member Since")]
    public DateTime MemberSince { get; set; } = DateTime.Today;
}
