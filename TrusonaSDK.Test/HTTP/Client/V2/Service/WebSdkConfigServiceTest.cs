//
// WebSdkConfigServiceTest.cs
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