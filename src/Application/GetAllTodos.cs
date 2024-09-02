using Domain;
using Ykvelit.Core.Application.Commands;

namespace Application;
public static class GetAllTodos
{
    public class Request : ICommand<IEnumerable<Todo>>;

    public class Handler(ITodoRepository repository) : ICommandHandler<Request, IEnumerable<Todo>>
    {
        public async Task<IEnumerable<Todo>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken);
        }
    }
}
