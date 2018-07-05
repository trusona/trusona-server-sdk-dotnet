//
// UserDeviceApiExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Net;
using System.Threading.Tasks;
using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;
namespace TrusonaSDK.API
{
  /// <summary>
  /// User device API.
  /// </summary>
  public static class UserDeviceExtension
  {
    /// <summary>
    /// Create a user device binding in Trusona between a User and a Device, referenced by their identifiers. After creation,
    /// the binding will be inactive, and must be explicitly activated before the User can use the Device to complete
    /// Trusonafications.
    /// </summary>
    /// <returns>The user device.</returns>
    /// <param name="trusona">The Trusona API.</param>
    /// <param name="userIdentifier">User identifier.</param>
    /// <param name="deviceIdentifier">Device identifier.</param>
    public static async Task<UserDevice> CreateUserDevice(this Trusona trusona, string userIdentifier, string deviceIdentifier)
    {
      UserDeviceRequest request = new UserDeviceRequest()
      {
        UserIdentifier = userIdentifier,
        DeviceIdentifier = deviceIdentifier
      };

      try
      {
        var response = await trusona.UserDeviceService.CreateUserDeviceAsync(request);
        return trusona.mapper.Map<UserDevice>(response);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, (httpStatus, requestId) =>
        {
          switch (httpStatus)
          {
            case HttpStatusCode.Conflict:
              throw new DeviceAlreadyBoundException("A different user has already been bound to this device.");
            case (HttpStatusCode)424:
              throw new DeviceNotFoundException("The device you are attempting to bind to a user does not exist. The device will need to be re-registered with Trusona before attempting to bind it again.");
            default:
              DefaultErrorHandler(httpStatus, requestId);
              break;
          }
        });
        throw ex;
      }
    }

    /// <summary>
    /// Activates a user device binding in Trusona. After a binding is active, a user can respond to Trusonafications.
    /// Only call this method after you have verified the identity of the user.
    /// </summary>
    /// <returns>True if the device is activated.</returns>
    /// <param name="trusona">The Trusona API.</param>
    /// <param name="activationCode">Activation code.</param>
    public static async Task<bool> ActivateUserDevice(this Trusona trusona, string activationCode)
    {
      UserDeviceUpdateRequest request = new UserDeviceUpdateRequest() { Active = true };
      try
      {
        var response = await trusona.UserDeviceService.UpdateUserDeviceAsync(activationCode, request);
        var result = trusona.mapper.Map<UserDevice>(response);
        return result.Active;
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}