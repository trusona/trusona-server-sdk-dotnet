//
// WebSdkConfigServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Service;
using Xunit;
using FluentAssertions;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class WebSdkConfigServiceTest : MockedServiceTest<WebSdkConfigService>
  {
    [Fact]
    public void GetWebSdkConfig_should_return_an_expected_config_string()
    {
      var res = sut.GetWebSdkConfig();
      res.Should()
         .Be(@"{""truCodeUrl"": ""https://jones.net"",""relyingPartyId"": ""0f0348f0-46d6-47c9-ba4d-2e7cd7f82e3e""}");
    }
  }
}