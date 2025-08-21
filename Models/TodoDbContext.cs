using Microsoft.EntityFrameworkCore;

namespace todo_app_asp_core.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<TodoItem>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Items)
                .HasForeignKey(s => s.UserId);
        }

        public DbSet<TodoItem> Items { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
