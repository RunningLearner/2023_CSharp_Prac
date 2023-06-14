using Microsoft.EntityFrameworkCore;
using SecondApi.Data;

namespace SecondApi.Services;

public sealed class TodoItemService
{
    private readonly TodoDbContext _context;
    private readonly ILogger<TodoItemService> _logger;

    public TodoItemService(TodoDbContext context, ILogger<TodoItemService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<TodoItemDto>> GetAllAsync()
    {
        var results = await _context.TodoItems
            .Select(x => ItemToDto(x))
            .ToListAsync();
        _logger.LogInformation("TodosCount : {Count}", results.Count);

        return results;
    }

    public async Task<TodoItemDto> GetAsync(long id)
    {
        var todoItem = await FindTodoByIdAsync(id);
        _logger.LogInformation("TodoId : {Id}", todoItem.Id);

        return ItemToDto(todoItem);
    }

    public async Task<TodoItemDto> AddAsync(TodoItemDto todoDto)
    {
        var todoItem = new TodoItem
        {
            IsComplete = todoDto.IsComplete,
            Name = todoDto.Name
        };

        _context.TodoItems.Add(todoItem);
        _logger.LogInformation("NewTodo : {@NewTodo}", todoDto);
        await _context.SaveChangesAsync();

        return ItemToDto(todoItem);
    }

    public async Task UpdateAsync(long id, TodoItemDto todoDto)
    {
        var todoItem = await FindTodoByIdAsync(id);
        todoItem.Name = todoDto.Name;
        todoItem.IsComplete = todoDto.IsComplete;
        _logger.LogInformation("UpdatedTodo : {@UpdatedTodo}", todoDto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var todoItem = await FindTodoByIdAsync(id);
        _context.TodoItems.Remove(todoItem);
        _logger.LogInformation("DeletedTodo : {@DeletedTodo}", todoItem);
        await _context.SaveChangesAsync();
    }

    private static TodoItemDto ItemToDto(TodoItem todoItem)
    {
        return new TodoItemDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }

    private async Task<TodoItem> FindTodoByIdAsync(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            _logger.LogError("UpdatedTodo : {Id} Not found", id);
            throw new KeyNotFoundException($"ID가 {id}인 Todo를 찾을 수 없습니다.");
        }

        return todoItem;
    }
}