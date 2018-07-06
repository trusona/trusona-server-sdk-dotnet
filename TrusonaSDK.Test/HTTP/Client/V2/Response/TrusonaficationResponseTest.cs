//
// TrusonaficationResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class TrusonaficationResponseTest : RequestResponseTest<TrusonaficationResponse>
  {
    public override TrusonaficationResponse Sut => new TrusonaficationResponse()
    {
      Id = Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"),
      Status = "ACCEPTED",
      UserIdentifier = "t-money",
      CreatedAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      UpdatedAt = DateTime.Parse("2018-01-23T23:28:46Z").ToUniversalTime(),
      DeviceIdentifier = "datDevice",
      DesiredLevel = 2,
      Action = "partay",
      Resource = "your hauz",
      ExpiresAt = DateTime.Parse("2018-01-23T23:28:47Z").ToUniversalTime(),
      CallbackUrl = "https://kid-and-play.com/",
      UserPresence = false,
      Prompt = false,
      Result = new TrusonaficationResultResponse()
      {
        Id = Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f8"),
        Accepted = true,
        AcceptedLevel = 2
      }
    };

    public override string Json => @"{
  ""action"": ""partay"",
  ""callback_url"": ""https://kid-and-play.com/"",
  ""created_at"": ""2018-01-23T23:28:45Z"",
  ""desired_level"": 2,
  ""device_identifier"": ""datDevice"",
  ""expires_at"": ""2018-01-23T23:28:47Z"",
  ""id"": ""96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"",
  ""prompt"": false,
  ""resource"": ""your hauz"",
  ""result"": {
    ""accepted_level"": 2,
    ""id"": ""96ea5830-8e5e-42c5-9cbb-8a941d2ff7f8"",
    ""is_accepted"": true
  },
  ""show_identity_document"": false,
  ""status"": ""ACCEPTED"",
  ""updated_at"": ""2018-01-23T23:28:46Z"",
  ""user_identifier"": ""t-money"",
  ""user_presence"": false
}";
  }
}
