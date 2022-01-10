using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace ToDoREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoSession _session;

        public TodoController(ILogger<TodoController> logger, ITodoSession session)
        {
            _logger = logger;
            _session = session;
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _session.Todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(Guid id)
        {
            Todo? todo = await _session.Todos.FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost]
        public async Task<Todo> Create(Todo todo)
        {
            todo.Id = Guid.Empty;
            await _session.Save(todo);
            return todo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> Update(Guid id, Todo updated)
        {
            try
            {
                _session.BeginTransaction();
                Todo? todo = await _session.Todos.FirstOrDefaultAsync(i => i.Id == id);
                if (todo == null)
                {
                    return NotFound();
                }
                todo.Title = updated.Title;
                todo.Summary = updated.Summary;
                await _session.Commit();
                return todo;
            }
            catch (Exception ex)
            {
                await _session.Rollback();
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            finally
            {
                _session.CloseTransaction();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Todo? todo = await _session.Todos.FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            await _session.Delete(todo);
            return NoContent();
        }
    }
}
