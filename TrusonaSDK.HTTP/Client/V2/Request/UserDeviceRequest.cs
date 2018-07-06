//
// UserDeviceRequestSpec.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class UserDeviceRequest : BaseRequestResponse
  {
    public string DeviceIdentifier { get; set; }
    public string UserIdentifier { get; set; }

    public UserDeviceRequest() { }
  }
}
