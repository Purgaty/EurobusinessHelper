using System.Reflection;
using EurobusinessHelper.Application.Common.Interfaces;
using EurobusinessHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EurobusinessHelper.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    public DbSet<Identity> Identities => Set<Identity>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
#if DEBUG
            .EnableSensitiveDataLogging()
#endif
            .UseLoggerFactory(_loggerFactory);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        SetTimestamps();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        SetTimestamps();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    private void SetTimestamps()
    {
        SetCreatedOnTimestamp();
        SetModifiedOnTimestamp();
    }

    private void SetCreatedOnTimestamp()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added);
        foreach (var entry in entries)
            if (entry.Entity is IEntity entity)
            {
                entity.CreatedOn = DateTime.Now;
                entity.ModifiedOn = DateTime.Now;
            }
    }

    private void SetModifiedOnTimestamp()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);
        foreach (var entry in entries)
            if (entry.Entity is IEntity entity)
                entity.ModifiedOn = DateTime.Now;
    }
}