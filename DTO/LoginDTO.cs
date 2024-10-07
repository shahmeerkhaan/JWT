using System.ComponentModel.DataAnnotations;

namespace Resume_Analyzer_Backend.Data.DTO;
public class LoginDTO
{
    [Required]
    public string UserName_Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}