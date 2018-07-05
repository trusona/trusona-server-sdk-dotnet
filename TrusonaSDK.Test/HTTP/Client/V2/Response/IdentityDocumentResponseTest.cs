//
// IdentityDocumentResponseTest.cs
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
