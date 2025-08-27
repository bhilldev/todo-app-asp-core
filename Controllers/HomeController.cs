//using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todo_app_asp_core.Models;
using todo_app_asp_core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace todo_app_asp_core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoController> _logger;

        public TodoController(TodoDbContext context, ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/authors (with related books)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users
                .Include(a => a.Items)
                .ToListAsync();
        }

        // GET: api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(a => a.Items)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
                return NotFound();

            return user;
        }
        [HttpGet("items/{id}")]
        public async Task<ActionResult<TodoItem>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();
            return item;
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // POST: api/authors/5/books
        //[HttpPost("{Id}/items")]
        //public async Task<ActionResult<TodoItem>> AddItemToUser(int userId, TodoItem item)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user == null)
        //        return NotFound();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    item.UserId = userId;
        //    _context.Items.Add(item);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetItem), new { id = userId }, item);
        //}
        [HttpPost("{userId}/items")]
        public async Task<ActionResult<TodoItem>> AddItemToUser(int userId, TodoItemCreateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            var item = new TodoItem
            {
                Entry = dto.Entry,
                isCompleted = dto.isCompleted,
                UserId = userId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
    }
}
