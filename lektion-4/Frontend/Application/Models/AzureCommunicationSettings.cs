namespace Application.Models;

public class AzureCommunicationSettings
{
    public string ConnectionString { get; set; } = null!;
    public string SenderAddress { get; set; } = null!;
    public string SenderName { get; set; } = null!;
}
