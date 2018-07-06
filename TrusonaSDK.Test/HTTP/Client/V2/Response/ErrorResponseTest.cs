//
// ErrorResponseTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class ErrorResponseTest : RequestResponseTest<ErrorResponse>
  {
    public override ErrorResponse Sut => new ErrorResponse()
    {
      Error = "error",
      Description = "description",
      Message = "message",
      FieldErrors = new Dictionary<string, List<string>>()
      {
        { "foo", new List<string>() { "bar", "fizz" } }
      }
    };

    public override string Json => @"{
  ""description"": ""description"",
  ""error"": ""error"",
  ""field_errors"": {
    ""foo"": [
      ""bar"",
      ""fizz""
    ]
  },
  ""message"": ""message""
}";
  }
}