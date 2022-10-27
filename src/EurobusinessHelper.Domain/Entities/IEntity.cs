namespace EurobusinessHelper.Domain.Entities;

public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}