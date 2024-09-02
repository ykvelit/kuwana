using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.RestApi.Endpoints.v1;

public static class GetAllTodosEndpoint
{
    public const string Pattern = "/api/v{version:apiVersion}/todos";

    public static async Task<IResult> Handler([FromServices] ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetAllTodos.Request();
        var result = await sender.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}
