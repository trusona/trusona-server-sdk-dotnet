//
// UserDeviceResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class UserDeviceResponseTest : RequestResponseTest<UserDeviceResponse>
  {
    public override UserDeviceResponse Sut => new UserDeviceResponse()
    {
      UserIdentifier = "user",
      DeviceIdentifier = "device",
      Id = Guid.Parse("92E22E18-879E-413D-9A2E-A0E5DA5B186D"),
      Active = true
    };

    public override string Json => @"{
  ""active"": true,
  ""device_identifier"": ""device"",
  ""id"": ""92E22E18-879E-413D-9A2E-A0E5DA5B186D"",
  ""user_identifier"": ""user""
}";
  }
}
