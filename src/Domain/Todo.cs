using MongoDB.Bson;

namespace Domain;
public class Todo
{
    protected Todo() { }

    public Todo(string title, string? description, DateTime dueDate) : base()
    {
        Id = ObjectId.GenerateNewId().ToString();
        Title = title;
        Description = description ?? string.Empty;
        DueDate = dueDate;
    }

    public string Id { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public DateTime DueDate { get; private set; }
    public bool IsCompleted { get; private set; } = false;

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdateDescription(string? description)
    {
        Description = description ?? string.Empty;
    }

    public void UpdateDueDate(DateTime dueDate)
    {
        DueDate = dueDate;
    }
}
