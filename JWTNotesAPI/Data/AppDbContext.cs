using Microsoft.EntityFrameworkCore;
using JWTNotesAPI.Models;

namespace JWTNotesAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Users table
        public DbSet<User> Users { get; set; }

        // Notes table
        public DbSet<Note> Notes { get; set; }
    }
}