namespace EurobusinessHelper.UI.ASP.RequestModels.TransferRequest;

public class CreateTransferRequestRequest
{
    public Guid AccountId { get; set; }
    public int Amount { get; set; }
}