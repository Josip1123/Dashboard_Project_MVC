using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class MemberEntity
{
    [Key] [Required] public string Id { get; set; } = null!;
    
    [Column(TypeName="nvarchar(50)")]
    public string FirstName { get; set; } = null!;
    
    [Column(TypeName="nvarchar(50)")]
    public string LastName { get; set; } = null!;
    
    [Column(TypeName="nvarchar(100)")]
    public string Email { get; set; } = null!;
    
    [DataType(DataType.Password)]
    [Column(TypeName="nvarchar(max)")]
    public string Password { get; set; } = null!;
    
    public string? Phone { get; set; }

    public string Title { get; set; } = null!;
}