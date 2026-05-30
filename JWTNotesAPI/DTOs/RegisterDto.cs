using System.ComponentModel.DataAnnotations;

namespace JWTNotesAPI.DTOs
{
    public class RegisterDto
    {
        // Username required
        [Required]

        // Minimum 4 characters
        [MinLength(4)]
        public string Username { get; set; }

        // Password required
        [Required]

        // Minimum length
        [MinLength(8)]

        // Password validation
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage =
            "Password must contain uppercase, number and special character.")]
        public string Password { get; set; }
    }
}