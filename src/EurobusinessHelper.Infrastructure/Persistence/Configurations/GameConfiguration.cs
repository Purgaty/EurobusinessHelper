using EurobusinessHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurobusinessHelper.Infrastructure.Persistence.Configurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(g => g.Name)
            .UseCollation(ConfigurationConsts.CaseInsensitiveAccentInsensitiveCollation)
            .HasMaxLength(200)
            .IsRequired();
    }
}