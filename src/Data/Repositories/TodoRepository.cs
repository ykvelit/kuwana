using Domain;
using Microsoft.EntityFrameworkCore;
using Ykvelit.Core.Exceptions;

namespace Data.Repositories;
public class TodoRepository(DbContext db) : ITodoRepository
{
    public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
    {
        var todo = await TryGetByIdAsync(id, cancellationToken);

        if (todo is not null)
        {
            db.Set<Todo>().Remove(todo);
        }
    }

    public async Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await db.Set<Todo>().ToListAsync(cancellationToken);
    }

    public async Task<Todo> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var todo = await TryGetByIdAsync(id, cancellationToken);
        return todo ??
            throw new NotFoundException(id, typeof(Todo), "To do item not found");
    }


    public Task InsertAsync(Todo todo, CancellationToken cancellationToken)
    {
        return db.AddAsync(todo, cancellationToken).AsTask();
    }

    public void Update(Todo todo)
    {
        db.Set<Todo>().Update(todo);
    }

    private async Task<Todo?> TryGetByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            return await db
                .Set<Todo>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch
        {
            return null;
        }
    }
}
