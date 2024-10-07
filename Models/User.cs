using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resume_Analyzer_Backend.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage="Username should have atleast 50 characters.")]
    [Required(ErrorMessage = "Cannot be leaved empty")]
    [Display(Name = "Username")]
    public string? Name { get; set; }

    [MaxLength(320, ErrorMessage = "Email characters can't be of more than 320.")]
    [Required(ErrorMessage = "Cannot be null!!!")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
    
    [StringLength(Int32.MaxValue)]
    [Required(ErrorMessage = "Password is Required!!!")]
    [Display(Name = "Password")]
    public string? Password { get; set; }
}
