using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.RestApi.Endpoints.v1;

public static class GetTodoByIdEndpoint
{
    public const string Pattern = "/api/v{version:apiVersion}/todos/{id}";

    public static async Task<IResult> Handler([FromRoute] string id, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetTodoById.Request(id);
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
