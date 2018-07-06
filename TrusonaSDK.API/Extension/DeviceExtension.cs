//
// DeviceExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;

namespace TrusonaSDK.API
{
  /// <summary>
  /// Devices API.
  /// </summary>
  public static class DeviceExtension
  {
    /// <summary>
    /// Gets the device.
    /// </summary>
    /// <returns>The device.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="deviceIdentifier">Device identifier.</param>
    public static async Task<Device> GetDevice(this Trusona trusona, string deviceIdentifier)
    {
      try
      {
        var response = await trusona.DeviceService.GetDeviceAsync(deviceIdentifier);
        var result = trusona.mapper.Map<Device>(response);
        return result;
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}