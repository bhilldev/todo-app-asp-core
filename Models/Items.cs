using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class Items 
    {
        public int Id { get; set; }

        public string Item { get; set; }

        public bool isCompleted { get; set; }

        [Column(TypeName = "timestamp")]

        public DateTime TimeAdded { get; set; } = DateTime.Now;
        
        public User User { get; set; }
    }
}
