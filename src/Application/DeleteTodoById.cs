using Domain;
using FluentValidation;
using MediatR;
using Ykvelit.Core.Application.Commands;

namespace Application;

public static class DeleteTodoById
{
    public record Request(string Id) : ICommand<Unit>;

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }

    public class Handler(ITodoRepository repository) : ICommandHandler<Request, Unit>
    {
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}