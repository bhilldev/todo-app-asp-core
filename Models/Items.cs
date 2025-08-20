using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Entry { get; set; }

        public bool isCompleted { get; set; }

        [Column(TypeName = "timestamp")]

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public User User { get; set; } = new();
    }
}
