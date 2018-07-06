//
// UserDeviceService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class UserDeviceService : HttpService, IUserDeviceService
  {
    private readonly ICredentialProvider _credentialProvider;

    public UserDeviceService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, environment.EndpointUrl)
    {
      this._credentialProvider = environment.CredentialProvider;
    }

    public UserDeviceResponse CreateUserDevice(UserDeviceRequest request)
    {
      return BlockAsyncForResult(
        CreateUserDeviceAsync(request)
      );
    }

    public Task<UserDeviceResponse> CreateUserDeviceAsync(UserDeviceRequest request)
    {
      return Post<UserDeviceResponse>(
        resource: "/api/v2/user_devices",
        content: request,
        credentialProvider: _credentialProvider
      );
    }

    public UserDeviceResponse UpdateUserDevice(string id, UserDeviceUpdateRequest request)
    {
      return BlockAsyncForResult(
        UpdateUserDeviceAsync(id, request)
      );
    }

    public Task<UserDeviceResponse> UpdateUserDeviceAsync(string id, UserDeviceUpdateRequest request)
    {
      return Patch<UserDeviceResponse>(
        id: id,
        resource: "/api/v2/user_devices",
        content: request,
        credentialProvider: _credentialProvider
      );
    }
  }
}