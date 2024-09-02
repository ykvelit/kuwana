using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.RestApi.Endpoints.v1;

public static class CreateTodoEndpoint
{
    public const string Pattern = "/api/v{version:apiVersion}/todos";

    public static async Task<IResult> Handler([FromBody] CreateTodo.Request command, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
