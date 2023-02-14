using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Games.Queries.GetGameAccounts;

public class GetGameDetailsQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Identity CreatedBy { get; set; }
    public bool IsActive { get; set; }
    public bool IsPasswordProtected { get; set; }
    public bool CanJoin { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public GameState State { get; set; }
    public ICollection<Account> Accounts { get; set; }
    public int AccountCount => Accounts.Count;
    public int StartingAccountBalance { get; set; }
    public int MinimalBankTransferApprovals { get; set; }
    
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
    }

    public class Identity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}