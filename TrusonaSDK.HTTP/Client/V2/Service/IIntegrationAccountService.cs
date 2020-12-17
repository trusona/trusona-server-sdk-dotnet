using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IIntegrationAccountService
  {
    Task<IntegrationAccountResponse> CreateIntegrationAccountAsync(Guid integrationId, IntegrationAccountRequest integrationAccount);

    IntegrationAccountResponse CreateIntegrationAccount(Guid integrationId, IntegrationAccountRequest integrationAccount);

    List<IntegrationAccountResponse> GetIntegrationAccounts(Guid integrationId, string identifier);

    Task<List<IntegrationAccountResponse>> GetIntegrationAccountsAsync(Guid integrationId, string identifier);
  }
}