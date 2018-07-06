//
// UserDeviceRequestTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class UserDeviceRequestTest : RequestResponseTest<UserDeviceRequest>
  {
    public override UserDeviceRequest Sut => new UserDeviceRequest()
    {
      UserIdentifier = "user",
      DeviceIdentifier = "device"
    };

    public override string Json => @"{
  ""device_identifier"": ""device"",
  ""user_identifier"": ""user""
}";
  }
}
