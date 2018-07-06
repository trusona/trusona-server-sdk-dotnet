//
// IDeviceService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IDeviceService
  {
    Task<DeviceResponse> GetDeviceAsync(string deviceIdentifier);

    DeviceResponse GetDevice(string deviceIdentifier);
  }
}
