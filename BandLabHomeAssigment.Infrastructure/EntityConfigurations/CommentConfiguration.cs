using BandLabHomeAssigment.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BandLabHomeAssigment.Domain.Validations;

namespace BandLabHomeAssigment.Infrastructure.EntityConfigurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Content).IsRequired().HasMaxLength(ValidationOptions.CommentContentMaxLength);
        builder.Property(c => c.CreatorId).IsRequired();
        builder.Property(c => c.PostId).IsRequired();
        builder.HasOne<Post>().WithMany(p => p.Comments).HasForeignKey(c => c.PostId);
    }
}
