using EurobusinessHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurobusinessHelper.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.Property(u => u.Email)
            .UseCollation(ConfigurationConsts.CaseInsensitiveAccentInsensitiveCollation)
            .HasMaxLength(260)
            .IsRequired();
        builder.Property(u => u.FirstName)
            .UseCollation(ConfigurationConsts.CaseInsensitiveAccentInsensitiveCollation)
            .HasMaxLength(200);
        builder.Property(u => u.LastName)
            .UseCollation(ConfigurationConsts.CaseInsensitiveAccentInsensitiveCollation)
            .HasMaxLength(200);
    }
}