//
// UserDeviceUpdateRequest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class UserDeviceUpdateRequest : BaseRequestResponse
  {
    public bool Active { get; set; }

    public UserDeviceUpdateRequest() { }
  }
}