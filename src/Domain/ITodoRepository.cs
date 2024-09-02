namespace Domain;
public interface ITodoRepository
{
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken);
    Task<Todo> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task InsertAsync(Todo todo, CancellationToken cancellationToken);
    void Update(Todo todo);
}
