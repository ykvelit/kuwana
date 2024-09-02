using Domain;
using FluentValidation;
using Ykvelit.Core.Application.Commands;

namespace Application;
public static class CreateTodo
{
    public record Request(string Title, string? Description, DateTime DueDate) : ICommand<string>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }

    public class Handler(ITodoRepository repository) : ICommandHandler<Request, string>
    {
        public async Task<string> Handle(Request request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Title, request.Description, request.DueDate);

            await repository.InsertAsync(todo, cancellationToken);

            return todo.Id.ToString();
        }
    }
}
