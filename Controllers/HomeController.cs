//using System.Diagnostics;
using System.Security.Claims;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            //var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (!int.TryParse(userIdClaim, out var userId))
            //{
            //    return Unauthorized();
            //}
            var userId = 2;
            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (item == null)
            {
                return NotFound(); // or Forbid() if you want to distinguish ownership
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemDto dto)
        {
            // Get logged-in user's ID
            //   var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //   if (!int.TryParse(userIdClaim, out var userId))
            //   {
            //       return Unauthorized();
            //   }


            // WARNING... userId value is hardcoded for testing purposes. JWT is not set up yet.
            // Above commented code can be uncommented when JWT is set up.
            var userId = 2;
            // Find the item by id and user
            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);

            if (item == null)
            {
                return NotFound();
            }

            // Update properties (only if provided)
            if (!string.IsNullOrWhiteSpace(dto.Entry))
            {
                item.Entry = dto.Entry;
            }

            item.isCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

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
