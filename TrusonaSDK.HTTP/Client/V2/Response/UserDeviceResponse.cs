//
// UserDeviceResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class UserDeviceResponse : BaseRequestResponse
  {
    public Guid Id { get; set; }
    public string DeviceIdentifier { get; set; }
    public string UserIdentifier { get; set; }
    public bool Active { get; set; }
    public string ActivationCode { get; set; }
  }
}
