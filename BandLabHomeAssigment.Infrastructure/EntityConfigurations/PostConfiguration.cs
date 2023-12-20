using BandLabHomeAssigment.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BandLabHomeAssigment.Domain.Validations;

namespace BandLabHomeAssigment.Infrastructure.EntityConfigurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Caption).IsRequired().HasMaxLength(ValidationOptions.PostCaptionMaxLength);
        builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(ValidationOptions.ImageUrlMaxLength);
        builder.Property(p => p.CreatorId).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(p => p.CreatorId);
        builder.HasMany(p => p.Comments).WithOne().HasForeignKey(c => c.PostId);
    }
}
