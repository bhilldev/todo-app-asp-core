namespace todo_app_asp_core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string password { get; set; }

        public ICollection<TodoItem> Items { get; } = new List<TodoItem>();
    }
}

