using System;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Service;

using static TrusonaSDK.API.Trusona;


namespace TrusonaSDK.API.Extension
{
  public static class IntegrationCredentialExtension
  {
    public static async Task<IntegrationCredentialResponse> CreateIntegrationCredential(this Trusona trusona, Guid integrationId, string identifier)
    {
      try
      {
        var response = await trusona.IntegrationCredentialService.CreateIntegrationCredentialAsync(integrationId, identifier);
        return trusona.mapper.Map<IntegrationCredentialResponse>(response);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}
