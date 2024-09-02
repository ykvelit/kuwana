using Domain;
using FluentValidation;
using MediatR;
using Ykvelit.Core.Application.Commands;

namespace Application;

public static class UpdateTodo
{
    public record Request(string Title, string? Description, DateTime DueDate) : ICommand<Unit>
    {
        public string Id { get; set; } = null!;
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }

    public class Handler(ITodoRepository repository) : ICommandHandler<Request, Unit>
    {
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);

            todo.UpdateTitle(request.Title);
            todo.UpdateDescription(request.Description);
            todo.UpdateDueDate(request.DueDate);

            repository.Update(todo);

            return Unit.Value;
        }
    }
}