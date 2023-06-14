namespace SecondApi.Data;

public record TodoItemDto
{
    public long Id { get; init; }
    public string? Name { get; init; }
    public bool IsComplete { get; init; }
}
