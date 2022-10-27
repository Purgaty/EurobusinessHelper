﻿namespace EurobusinessHelper.Domain.Entities;

public class Game : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Identity CreatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}