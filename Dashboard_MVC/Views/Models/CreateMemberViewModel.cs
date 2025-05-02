using System.ComponentModel.DataAnnotations;

namespace Dashboard_MVC.Views.Models;

public class CreateMemberViewModel
{
    
    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50, ErrorMessage = "First Name must be 50 characters or less.")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50, ErrorMessage = "Last Name must be 50 characters or less.")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "email is required.")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;
    
    [StringLength(50, ErrorMessage = "Phone number must be 50 characters or less.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Phone number can only contain digits.")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Job title is required.")]
    [StringLength(50, ErrorMessage = "Job title must be 50 characters or less.")] 
    public string? Title { get; set; }


    public IFormFile? ImageFile { get; set; }
    public IEnumerable<Data.Entities.MemberEntity> Members { get; set; } = new List<Data.Entities.MemberEntity>();
}