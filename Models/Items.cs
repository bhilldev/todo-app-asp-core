using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Entry { get; set; }

        public bool isCompleted { get; set; }

        [Column(TypeName = "timestamp")]

        public DateTime TimeAdded { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}
