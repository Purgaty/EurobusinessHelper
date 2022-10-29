using EurobusinessHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EurobusinessHelper.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Identity> Identities { get; }
    DbSet<Game> Games { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Transaction> Transactions { get; }

    public Task<int> SaveChangesAsync(CancellationToken token = default);
}