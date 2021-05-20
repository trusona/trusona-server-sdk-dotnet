using System;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IIntegrationCredentialService
  {
    IntegrationCredentialResponse CreateIntegrationCredential(Guid integrationId, string identifier);

    Task<IntegrationCredentialResponse> CreateIntegrationCredentialAsync(Guid integrationId, string identifier);
  }
}