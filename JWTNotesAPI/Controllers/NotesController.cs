using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using JWTNotesAPI.Data;
using JWTNotesAPI.DTOs;
using JWTNotesAPI.Models;

using System.Security.Claims;

namespace JWTNotesAPI.Controllers
{
    [ApiController]

    // Base route
    [Route("api/notes")]

    // Protect all endpoints using JWT
    [Authorize]

    public class NotesController : ControllerBase
    {
        // Database object
        private readonly AppDbContext _context;

        // Constructor
        public NotesController(AppDbContext context)
        {
            _context = context;
        }


        // ADD NOTE API
        [HttpPost]
        public async Task<IActionResult> AddNote(NoteDto dto)
        {
            // Get logged-in user ID from JWT token
            var userId = int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value!);

            // Create note object
            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,

                // Assign note owner
                UserId = userId
            };

            // Add note to database
            _context.Notes.Add(note);

            // Save changes
            await _context.SaveChangesAsync();

            // Success response
            return Ok(new
            {
                message = "Note added successfully.",
                noteId = note.Id
            });
        }


        // GET ALL NOTES API
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            // Logged-in user ID
            var userId = int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value!);

            // Get only current user's notes
            var notes = await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return Ok(notes);
        }


        // UPDATE NOTE API
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(
            int id,
            NoteDto dto)
        {
            // Logged-in user ID
            var userId = int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value!);

            // Find note belonging to current user
            var note = await _context.Notes
                .FirstOrDefaultAsync(
                    n => n.Id == id &&
                    n.UserId == userId);

            // If note not found
            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found."
                });
            }

            // Update note data
            note.Title = dto.Title;
            note.Content = dto.Content;

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Note updated successfully."
            });
        }


        // DELETE NOTE API
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            // Logged-in user ID
            var userId = int.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value!);

            // Find note belonging to current user
            var note = await _context.Notes
                .FirstOrDefaultAsync(
                    n => n.Id == id &&
                    n.UserId == userId);

            // If note not found
            if (note == null)
            {
                return NotFound(new
                {
                    message = "Note not found."
                });
            }

            // Remove note
            _context.Notes.Remove(note);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Note deleted successfully."
            });
        }
    }
}