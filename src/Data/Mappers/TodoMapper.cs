using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Data.Mappers;
internal class TodoMapper : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToCollection("todos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasElementName("_id")
            .HasBsonRepresentation(BsonType.ObjectId);

        builder.Property(x => x.Title)
            .HasElementName("title");

        builder.Property(x => x.Description)
            .HasElementName("description");

        builder.Property(x => x.DueDate)
            .HasElementName("dueDate");

        builder.Property(x => x.IsCompleted)
            .HasElementName("isCompleted");
    }
}
