using System.ComponentModel.DataAnnotations;

namespace JWTNotesAPI.DTOs
{
    public class NoteDto
    {
        // Title required
        [Required]
        public string Title { get; set; }

        // Content required
        [Required]
        public string Content { get; set; }
    }
}