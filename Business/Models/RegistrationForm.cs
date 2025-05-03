using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class RegistrationForm
{
    [Required(ErrorMessage = "First name is required")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Last name is required")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*\\d)(?=.*\\W).{8,}$", ErrorMessage = "8 characters, one number and one special character")]
    public string Password { get; set; } = null!;
    
    
    [Required(ErrorMessage = "You need to accept terms")]
    public bool Terms { get; set; } 
}