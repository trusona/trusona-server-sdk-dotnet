//
// DeviceService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class DeviceService : HttpService, IDeviceService
  {
    private readonly ICredentialProvider _credentialProvider;

    public DeviceService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, environment.EndpointUrl)
    {
      this._credentialProvider = environment.CredentialProvider;
    }

    public DeviceResponse GetDevice(string deviceIdentifier)
    {
      return BlockAsyncForResult(
        GetDeviceAsync(deviceIdentifier)
      );
    }

    public Task<DeviceResponse> GetDeviceAsync(string deviceIdentifier)
    {
      return Get<DeviceResponse>(
        id: deviceIdentifier,
        resource: "/api/v2/devices",
        credentialProvider: _credentialProvider
      );
    }
  }
}