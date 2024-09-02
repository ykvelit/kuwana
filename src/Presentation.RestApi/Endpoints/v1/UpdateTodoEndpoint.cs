using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.RestApi.Endpoints.v1;

public static class UpdateTodoEndpoint
{
    public const string Pattern = "/api/v{version:apiVersion}/todos/{id}";

    public static async Task<IResult> Handler([FromRoute] string id, [FromBody] UpdateTodo.Request command, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
