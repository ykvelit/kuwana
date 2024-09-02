using Domain;
using FluentValidation;
using Ykvelit.Core.Application.Commands;

namespace Application;

public static class GetTodoById
{
    public record Request(string Id) : ICommand<Todo>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }

    public class Handler(ITodoRepository repository) : ICommandHandler<Request, Todo>
    {
        public async Task<Todo> Handle(Request request, CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
            return todo;
        }
    }
}
