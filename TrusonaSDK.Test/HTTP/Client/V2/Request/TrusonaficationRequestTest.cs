//
// TrusonaficationRequestTest.cs
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
using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class TrusonaficationRequestTest : RequestResponseTest<TrusonaficationRequest>
  {
    public override TrusonaficationRequest Sut => new TrusonaficationRequest()
    {
      DeviceIdentifier = "datDevice",
      UserIdentifier = "datUser",
      TruCodeId = "3827D5E5-B6C1-49F8-865E-72794D10BEF4",
      Email = "jones@tacos.com",
      DesiredLevel = 2,
      Action = "partay",
      Resource = "yourhauz",
      ExpiresAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      CallbackUrl = "https://kid-and-play.com/",
      UserPresence = false,
      Prompt = false,
      ShowIdentityDocument = true
    };

    public override string Json => @"{
  ""action"": ""partay"",
  ""callback_url"": ""https://kid-and-play.com/"",
  ""desired_level"": 2,
  ""device_identifier"": ""datDevice"",
  ""email"": ""jones@tacos.com"",
  ""expires_at"": ""2018-01-23T23:28:45Z"",
  ""prompt"": false,
  ""resource"": ""yourhauz"",
  ""show_identity_document"": true,
  ""trucode_id"": ""3827D5E5-B6C1-49F8-865E-72794D10BEF4"",
  ""user_identifier"": ""datUser"",
  ""user_presence"": false
}";
  }
}