using Data;
using Data.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Ykvelit.Core.Data;
using Ykvelit.Extensions.Data.EntityFrameworkCore;

namespace CrossCutting.DependencyInjection;

public static class DataModule
{
    public static IServiceCollection AddDataModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var client = new MongoClient(connectionString);

        services.AddSingleton(client);
        services.AddDbContext<DbContext, DatabaseContext>(builder => builder.UseMongoDB(client, "kuwana"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}