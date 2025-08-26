//using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todo_app_asp_core.Models;
using Microsoft.EntityFrameworkCore;

namespace todo_app_asp_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoDbContext _context;

        public TodoController(ILogger<TodoController> logger, TodoDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //Get Items
        [HttpGet("TodoItem/GetItem")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItem()
        {
            return await _context.Items.OrderBy(p => p.DateAdded).ToListAsync();
        }
        [HttpGet("TodoItem/GetItem/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        //Get Users-----
        [HttpGet("User/GetUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<TodoItem>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }
        [HttpPost]
        public async Task<IActionResult> PostItem(TodoItem item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        [HttpPut("Todo/PutUser{id}")]
        public async Task<IActionResult> PutItem(int id, TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItems(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
