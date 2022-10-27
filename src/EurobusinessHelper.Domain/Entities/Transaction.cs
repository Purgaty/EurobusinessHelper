﻿namespace EurobusinessHelper.Domain.Entities;

public class Transaction : IEntity
{
    public Guid Id { get; set; }
    public virtual Account From { get; set; }
    public virtual Account To { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}