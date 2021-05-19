namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class IntegrationCredentialResponse : BaseRequestResponse
  {
    public string Token { get; set; }

    public string Secret { get; set; }

    public long ExpiresIn { get; set; }
  }
}
