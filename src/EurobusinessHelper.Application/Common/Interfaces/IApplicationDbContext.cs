using System.Data;
using EurobusinessHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EurobusinessHelper.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Identity> Identities { get; }
    DbSet<Game> Games { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Domain.Entities.TransferRequest> TransferRequest { get; }

    public Task<int> SaveChangesAsync(CancellationToken token = default);
    IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
}