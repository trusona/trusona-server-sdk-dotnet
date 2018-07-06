//
// IdentityDocumentResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class IdentityDocumentResponseTest : RequestResponseTest<IdentityDocumentResponse>
  {
    public override IdentityDocumentResponse Sut => new IdentityDocumentResponse()
    {
      Id = Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f8"),
      Hash = "foobar",
      VerifiedAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      VerificationStatus = "UNVERIFIED",
      Type = "AAMVA_DRIVERS_LICENSE"
    };

    public override string Json => @"{
  ""hash"": ""foobar"",
  ""id"": ""96ea5830-8e5e-42c5-9cbb-8a941d2ff7f8"",
  ""type"": ""AAMVA_DRIVERS_LICENSE"",
  ""verification_status"": ""UNVERIFIED"",
  ""verified_at"": ""2018-01-23T23:28:45Z""
}";
  }
}
