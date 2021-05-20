using System;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class IntegrationCredentialService : HttpService, IIntegrationCredentialService
  {
    private readonly ICredentialProvider credentialProvider;

    private static string EndpointTemplate => "/api/v2/integrations/{0}/credentials";

    public IntegrationCredentialService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl) => credentialProvider = configuration.CredentialProvider;

    public IntegrationCredentialResponse CreateIntegrationCredential(Guid integrationId, string identifier) => BlockAsyncForResult(CreateIntegrationCredentialAsync(integrationId, identifier));

    public Task<IntegrationCredentialResponse> CreateIntegrationCredentialAsync(Guid integrationId, string identifier)
    {
      return Post<IntegrationCredentialResponse>(
     resource: string.Format(EndpointTemplate, integrationId),
     content: new IntegrationCredentialRequest { AccountIdentifier = identifier },
     credentialProvider: credentialProvider
     );
    }
  }
}
