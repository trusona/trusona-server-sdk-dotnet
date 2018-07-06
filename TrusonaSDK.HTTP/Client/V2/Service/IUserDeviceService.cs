//
// IUserDeviceService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IUserDeviceService
  {
    Task<UserDeviceResponse> CreateUserDeviceAsync(UserDeviceRequest request);

    Task<UserDeviceResponse> UpdateUserDeviceAsync(string id, UserDeviceUpdateRequest request);

    UserDeviceResponse CreateUserDevice(UserDeviceRequest request);

    UserDeviceResponse UpdateUserDevice(string id, UserDeviceUpdateRequest request);
  }
}
