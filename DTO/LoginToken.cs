namespace Resume_Analyzer_Backend.Data.DTO;
public class LoginToken
{
    public string? access_token { get; set; }
    public string? user_id { get; set; }
    public string? token_type { get; set; }
    public int? expires_in { get; set; }
}