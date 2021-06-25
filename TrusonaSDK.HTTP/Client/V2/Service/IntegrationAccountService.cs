using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class IntegrationAccountService : HttpService, IIntegrationAccountService
  {
    private readonly ICredentialProvider credentialProvider;

    private static string EndpointTemplate => "/api/v2/integrations/{0}/accounts";

    public IntegrationAccountService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl) => credentialProvider = configuration.CredentialProvider;

    public IntegrationAccountResponse CreateIntegrationAccount(Guid integrationId, IntegrationAccountRequest integrationAccountRequest) => BlockAsyncForResult(CreateIntegrationAccountAsync(integrationId, integrationAccountRequest));

    public Task<IntegrationAccountResponse> CreateIntegrationAccountAsync(Guid integrationId, IntegrationAccountRequest integrationAccountRequest)
    {
      return Post<IntegrationAccountResponse>(
        resource: string.Format(EndpointTemplate, integrationId),
        content: integrationAccountRequest,
        credentialProvider: credentialProvider
        );
    }

    public List<IntegrationAccountResponse> GetIntegrationAccounts(Guid integrationId, string[] identifiers) => BlockAsyncForResult(GetIntegrationAccountsAsync(integrationId, identifiers));

    public Task<List<IntegrationAccountResponse>> GetIntegrationAccountsAsync(Guid integrationId, string[] identifiers)
    {
      List<Tuple<string, object>> queryParams = new List<Tuple<string, object>>();

      foreach (string identifier in identifiers)
      {
        queryParams.Add(new Tuple<string, object>("identifier[]", identifier));
      }

      return Get<List<IntegrationAccountResponse>>(string.Format(EndpointTemplate, integrationId), credentialProvider, queryParams);
    }
  }
}