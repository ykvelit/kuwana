using Asp.Versioning;
using CrossCutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Presentation.RestApi.Endpoints.v1;
using Serilog;
using Ykvelit.Core.Data;
using Ykvelit.Extensions.AspNetCore.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        ;
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddRootModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName!.Replace("+","."));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("*", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ;
    });
});

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();
builder.Services.AddExceptionHandler<UserFriendlyExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DbContext>();
    await db.Database.EnsureCreatedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseExceptionHandler(opt => { });

app.UseCors("*");

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    await next();
    if (context.Request.Method == HttpMethods.Post)
    {
        await context.RequestServices.GetRequiredService<IUnitOfWork>().CommitAsync();
    }
});

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

app.MapPost(CreateTodoEndpoint.Pattern, CreateTodoEndpoint.Handler)
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1);
app.MapGet(GetTodoByIdEndpoint.Pattern, GetTodoByIdEndpoint.Handler)
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1);
app.MapGet(GetAllTodosEndpoint.Pattern, GetAllTodosEndpoint.Handler)
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1);
app.MapPut(UpdateTodoEndpoint.Pattern, UpdateTodoEndpoint.Handler)
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1);
app.MapDelete(DeleteTodoByIdEndpoint.Pattern, DeleteTodoByIdEndpoint.Handler)
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1);

await app.RunAsync();