using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Data;
using TaskEntity = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskEntity task)
        {
            if (task == null)
            {
                return BadRequest(new { Message = "Invalid task data." });
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // PUT: api/Tasks/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskEntity updatedTask)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DueDate = updatedTask.DueDate;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound(new { Message = "Task not found." });
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
