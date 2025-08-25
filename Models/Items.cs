using System.ComponentModel.DataAnnotations.Schema;

namespace todo_app_asp_core.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Entry { get; set; } = string.Empty;

        public bool isCompleted { get; set; }

        [Column(TypeName = "timestamp")]

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
