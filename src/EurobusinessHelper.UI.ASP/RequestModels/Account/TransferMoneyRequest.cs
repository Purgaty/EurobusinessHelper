﻿using System.ComponentModel.DataAnnotations;

namespace EurobusinessHelper.UI.ASP.RequestModels.Account;

/// <summary>
/// Transfer money request
/// </summary>
public class TransferMoneyRequest
{
    /// <summary>
    /// Account id or empty for bank transfers
    /// </summary>
    public Guid? AccountId { get; set; }
    
    /// <summary>
    /// Amount of transferred money
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }
}