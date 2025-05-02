using System.ComponentModel.DataAnnotations;
using Data.Entities;

namespace Dashboard_MVC.Views.Models;

public class EditFormViewModel
{
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "Project Name is required.")]
    [StringLength(50, ErrorMessage = "Project Name must be 50 characters or less.")]
    public string ProjectName { get; set; } = null!;

    [Required(ErrorMessage = "Client Name is required.")]
    [StringLength(50, ErrorMessage = "Client Name must be 50 characters or less.")]
    public string ClientName { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(150, ErrorMessage = "Description must be 150 characters or less.")]
    public string Description { get; set; } = null!;


    [DataType(DataType.Date)] 
    public DateTime DateDue { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime DateCreated { get; set; }
    
    public bool IsCompleted { get; set; }
    
    [Required(ErrorMessage = "Price is required.")]
    public decimal? Price { get; set; }

    public List<MemberEntity>? Members { get; set; } = new();

    public List<string>? AssignedMembers { get; set; } = new();
}