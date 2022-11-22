using System.ComponentModel.DataAnnotations;

namespace EurobusinessHelper.UI.ASP.RequestModels.GameManagement;

/// <summary>
/// Transfer money request
/// </summary>
public class TransferMoneyRequest
{
    /// <summary>
    /// Account id
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }
    
    /// <summary>
    /// Amount of transferred money
    /// </summary>
    [Required]
    public int Amount { get; set; }
}