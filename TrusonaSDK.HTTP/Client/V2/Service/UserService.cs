using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class UserService : HttpService, IUserService
  {
    private readonly ICredentialProvider _credentialProvider;

    public UserService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl)
    {
      this._credentialProvider = configuration.CredentialProvider;
    }

    public void DeleteUser(string userIdentifier)
    {
      BlockAsyncForResult(
        DeleteUserAsync(userIdentifier)
      );
    }

    public async Task DeleteUserAsync(string userIdentifier)
    {
      await Delete(
        id: userIdentifier,
        resource: "/api/v2/users",
        credentialProvider: _credentialProvider
      );
    }

  }
}