//
// UserBindingService.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class UserBindingService : HttpService, IUserBindingService
  {
    private readonly ICredentialProvider _credentialProvider;

    public UserBindingService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl)
    {
      this._credentialProvider = configuration.CredentialProvider;
    }

    public void CreateUserBinding(UserBindingRequest request)
    {
      BlockAsyncForResult(
        CreateUserBindingAsync(request)
      );
    }

    public async Task CreateUserBindingAsync(UserBindingRequest request)
    {
      await Post("/api/v2/user_bindings", request, _credentialProvider);
    }

  }
}
