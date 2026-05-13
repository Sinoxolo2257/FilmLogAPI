using Microsoft.Extensions.Configuration.UserSecrets;

namespace FilmLogAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
