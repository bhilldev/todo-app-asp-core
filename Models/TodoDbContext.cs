using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace TodoSite.Models
{
    public class TodoDbContext : DbContext
    { 
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
               
    }
}
