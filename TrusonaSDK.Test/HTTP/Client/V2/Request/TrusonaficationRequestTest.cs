//
// TrusonaficationRequestTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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
      EmailAddress = "jones@tacos.com",
      DesiredLevel = 2,
      Action = "partay",
      Resource = "yourhauz",
      ExpiresAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      UserPresence = false,
      Prompt = false,
      ShowIdentityDocument = true
    };

    public override string Json => @"{
  ""action"": ""partay"",
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