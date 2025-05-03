using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    
    [PersonalData]
    [Column(TypeName="nvarchar(50)")]
    public string FirstName { get; set; } = null!;
    
    [PersonalData]
    [Column(TypeName="nvarchar(50)")]
    public string LastName { get; set; } = null!;
    
}