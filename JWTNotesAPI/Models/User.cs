using System.ComponentModel.DataAnnotations;

namespace JWTNotesAPI.Models
{
    public class User
    {
        // Primary key
        public int Id { get; set; }

        // Username required
        [Required]

        // Minimum 4 characters
        [MinLength(4)]
        public string Username { get; set; }

        // Stores hashed password
        [Required]
        public string PasswordHash { get; set; }

        // One user can have many notes
        public List<Note> Notes { get; set; }
    }
}