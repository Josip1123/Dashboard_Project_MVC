using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key] [Required] public string Id { get; set; } = null!;
    
    [Column(TypeName="nvarchar(50)")]
    public string ProjectName { get; set; } = null!;
    
    [Column(TypeName="nvarchar(50)")]
    public string ClientName { get; set; } = null!;
    
    [Column(TypeName="nvarchar(150)")]
    public string Description { get; set; } = null!;
    
    public string DateCreated { get; set; } = DateTime.Now.ToString("d");
    
    public string DateDue { get; set; } = null!;
    
    //list of type Member
    
    public decimal Price { get; set; }
    
}