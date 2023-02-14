namespace EurobusinessHelper.Domain.Entities;

public class TransferRequest : IEntity
{
    public Guid Id { get; set; }
    public Account Account { get; set; }
    public int Amount { get; set; }
    public int ApprovalCount { get; set; }
    public bool Approved { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}