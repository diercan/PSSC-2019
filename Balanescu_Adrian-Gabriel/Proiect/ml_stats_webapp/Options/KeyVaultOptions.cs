namespace ml_stats_webapp.Options
{

  public enum KeyVaultUsage
  {
    UseLocalSecretStorage,
    UseClientSecret,
    UseMsi
  }
  
  public class KeyVaultOptions
  {
    public KeyVaultUsage Mode { get; set; }
    public string KeyVaultUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
  }
}