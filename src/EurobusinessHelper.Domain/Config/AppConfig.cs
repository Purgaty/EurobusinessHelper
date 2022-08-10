namespace EurobusinessHelper.Domain.Config;

public class AppConfig
{
    public ICollection<AuthenticationType> ActiveAuthenticationTypes { get; set; } = new List<AuthenticationType>();
}