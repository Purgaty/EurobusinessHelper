using System.ComponentModel.DataAnnotations.Schema;

namespace EurobusinessHelper.Domain.Entities;

[Table("Transaction")]
public class Transaction
{
    public Guid Id { get; set; }
    public Account From { get; set; }
    public Account To { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedOn { get; set; }
}