//
// DeviceResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class DeviceResponseTest : RequestResponseTest<DeviceResponse>
  {
    public override DeviceResponse Sut => new DeviceResponse()
    {
      ActivatedAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      Active = true
    };

    public override string Json => @"{
  ""activated_at"": ""2018-01-23T23:28:45Z"",
  ""is_active"": true
}";
  }
}