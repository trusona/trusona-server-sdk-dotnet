//
// TruCodeResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class TruCodeResponseTest : RequestResponseTest<TruCodeResponse>
  {
    public override TruCodeResponse Sut => new TruCodeResponse()
    {
      Id = Guid.Parse("EB06CE6A-99D5-4249-9026-70D10A08AB19"),
      Identifier = "foobar"
    };

    public override string Json => @"{
  ""id"": ""EB06CE6A-99D5-4249-9026-70D10A08AB19"",
  ""identifier"": ""foobar""
}";
  }
}