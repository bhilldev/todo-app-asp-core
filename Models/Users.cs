namespace TodoApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string password { get; set; }

        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
