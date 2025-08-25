namespace todo_app_asp_core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; } = string.Empty;

        public string lastName { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public ICollection<TodoItem> Items { get; } = new List<TodoItem>();
    }
}

