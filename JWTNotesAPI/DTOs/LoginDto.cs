using System.ComponentModel.DataAnnotations;

namespace JWTNotesAPI.DTOs
{
    public class LoginDto
    {
        // Username required
        [Required]
        public string Username { get; set; }

        // Password required
        [Required]
        public string Password { get; set; }
    }
}