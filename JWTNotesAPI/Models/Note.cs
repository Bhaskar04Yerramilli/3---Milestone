using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTNotesAPI.Models
{
    public class Note
    {
        // Primary key
        public int Id { get; set; }

        // Note title required
        [Required]
        public string Title { get; set; }

        // Note content required
        [Required]
        public string Content { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}