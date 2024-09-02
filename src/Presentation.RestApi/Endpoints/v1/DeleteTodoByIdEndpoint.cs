using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.RestApi.Endpoints.v1;

public static class DeleteTodoByIdEndpoint
{
    public const string Pattern = "/api/v{version:apiVersion}/todos/{id}";

    public static async Task<IResult> Handler([FromRoute] string id, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var command = new DeleteTodoById.Request(id);
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
