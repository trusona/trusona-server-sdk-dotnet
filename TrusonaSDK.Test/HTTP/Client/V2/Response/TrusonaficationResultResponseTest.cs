//
// TrusonaficationResultResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class TrusonaficationResultResponseTest : RequestResponseTest<TrusonaficationResultResponse>
  {
    public override TrusonaficationResultResponse Sut => new TrusonaficationResultResponse()
    {
      AcceptedLevel = 2,
      Id = Guid.Parse("92E22E18-879E-413D-9A2E-A0E5DA5B186D"),
      Accepted = true
    };

    public override string Json => @"{
  ""accepted_level"": 2,
  ""id"": ""92E22E18-879E-413D-9A2E-A0E5DA5B186D"",
  ""is_accepted"": true
}";
  }
}
