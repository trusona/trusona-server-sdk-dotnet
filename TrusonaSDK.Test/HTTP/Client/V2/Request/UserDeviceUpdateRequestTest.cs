//
// UserDeviceUpdateRequestTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class UserDeviceUpdateRequestTest : RequestResponseTest<UserDeviceUpdateRequest>
  {
    public override UserDeviceUpdateRequest Sut => new UserDeviceUpdateRequest()
    {
      Active = true
    };

    public override string Json => @"{
  ""active"": true
}";
  }
}
