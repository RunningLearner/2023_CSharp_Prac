using Microsoft.AspNetCore.Mvc;
using SecondApi.Data;
using SecondApi.Services;

namespace SecondApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TodoItemsController : ControllerBase
{
    private readonly TodoItemService _service;
    private readonly ILogger<TodoItemsController> _logger;

    public TodoItemsController(TodoItemService service, ILogger<TodoItemsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        try
        {
            return Ok(await _service.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "서버 에러 발생");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        try
        {
            var todoItem = await _service.GetAsync(id);
            return todoItem;
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "서버 에러 발생");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoDto)
    {
        if (id != todoDto.Id)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateAsync(id, todoDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "서버 에러 발생");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoDto)
    {
        try
        {
            var todoItem = await _service.AddAsync(todoDto);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "서버 에러 발생");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "서버 에러 발생");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
