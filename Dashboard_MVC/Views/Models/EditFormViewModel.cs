using System.ComponentModel.DataAnnotations;

namespace Dashboard_MVC.Views.Models;

public class EditFormViewModel
{
    public string Id { get; set; } = null!;
    
    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(50, ErrorMessage = "Project Name must be 50 characters or less.")]
    public string ProjectName { get; set; }

    [Required(ErrorMessage = "Client Name is required.")]
    [StringLength(50, ErrorMessage = "Client Name must be 50 characters or less.")]
    public string ClientName { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(150, ErrorMessage = "Description must be 150 characters or less.")]
    public string Description { get; set; }


    [DataType(DataType.Date)] 
    public DateTime DateDue { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public bool IsCompleted { get; set; }
    
    [Required(ErrorMessage = "Price is required.")]
    public decimal? Price { get; set; }
}