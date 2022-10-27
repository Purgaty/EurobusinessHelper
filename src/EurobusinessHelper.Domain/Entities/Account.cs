namespace EurobusinessHelper.Domain.Entities;
public class Account : IEntity
{
    public Guid Id { get; set; }
    public int Balance { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public virtual Identity Owner { get; set; }
    public virtual Game Game { get; set; }
}