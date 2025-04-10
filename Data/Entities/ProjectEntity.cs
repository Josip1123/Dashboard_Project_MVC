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
    
    public DateTime DateCreated { get; set; }
    
    public DateTime DateDue { get; set; } 

    public bool IsCompleted { get; set; }
    
    //list of type Member
    
    public decimal Price { get; set; }
    
}