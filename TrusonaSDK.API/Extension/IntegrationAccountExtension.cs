using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;

using static TrusonaSDK.API.Trusona;

namespace TrusonaSDK.API.Extension
{
  public static class IntegrationAccountExtension
  {
    public static async Task<IntegrationAccount> CreateIntegrationAccount(this Trusona trusona, Guid integrationId, IntegrationAccount integrationAccount)
    {
      try
      {
        var request = trusona.mapper.Map<IntegrationAccountRequest>(integrationAccount);
        var response = await trusona.IntegrationAccountService.CreateIntegrationAccountAsync(integrationId, request);
        return trusona.mapper.Map<IntegrationAccount>(response);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }

    public static async Task<List<IntegrationAccount>> GetIntegrationAccounts(this Trusona trusona, Guid integrationId, string identifier)
    {
      try
      {
        var response = await trusona.IntegrationAccountService.GetIntegrationAccountsAsync(integrationId, identifier);
        return trusona.mapper.Map<List<IntegrationAccount>>(response);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}