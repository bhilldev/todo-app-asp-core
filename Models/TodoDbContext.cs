using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItem> Items { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
