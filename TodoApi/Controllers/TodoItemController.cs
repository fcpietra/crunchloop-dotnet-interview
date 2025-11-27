using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/todolist/{idList}/todoitem/")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoItemController(TodoContext context)
        {
            _context = context;
        }

        //POST: api/todolist/5/todoitem/
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CreateTodoItem payload, long idList)
        {
            if (!TodoListExists(idList))
            {
                return NotFound();
            }

            var todoItem = new TodoItem
            {
                ItemDescription = payload.itemDescription,
                IdList = idList,
                Done = false
            };

            _context.ItemLists.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostTodoItem), new { id = todoItem.Id }, todoItem);
        }

        //GET: api/todolist/5/todoitem/
        [HttpGet]
        public async Task<ActionResult<IList<TodoItem>>> GetTodoItems(long idList)
        {
            if (!TodoListExists(idList))
            {
                return NotFound();
            }
            var todoItems = await _context.ItemLists
                .Where(item => item.IdList == idList)
                .ToListAsync();
            return Ok(todoItems);
        }

        //GET: api/todolist/5/todoitem/10
        [HttpGet("{idItem}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long idList, long idItem)
        {
            var todoItem = await _context.ItemLists.FindAsync(idItem);
            if (todoItem == null || todoItem.IdList != idList)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        //PUT: api/todolist/5/todoitem/10
        [HttpPut("{idItem}")]
        public async Task<ActionResult> PutTodoItem(long idList, long idItem, [FromBody] UpdateTodoItem payload)
        {
            var todoItem = await _context.ItemLists.FindAsync(idItem);
            if (todoItem == null || todoItem.IdList != idList)
            {
                return NotFound();
            }
            todoItem.ItemDescription = payload.ItemDescription;
            todoItem.Done = payload.Done;
            await _context.SaveChangesAsync();
            return Ok(todoItem);
        }

        //DELETE: api/todolist/5/todoitem/10
        [HttpDelete("{idItem}")]
        public async Task<ActionResult> DeleteTodoItem(long idList, long idItem)
        {
            var todoItem = await _context.ItemLists.FindAsync(idItem);
            if (todoItem == null || todoItem.IdList != idList)
            {
                return NotFound();
            }
            _context.ItemLists.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Métodos privados
        private bool TodoListExists(long id)
        {
            return _context.TodoList.Any(e => e.Id == id);
        }
    }
}
