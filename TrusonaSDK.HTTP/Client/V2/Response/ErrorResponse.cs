//
// ErrorResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class ErrorResponse : BaseRequestResponse
  {
    public string Error { get; set; }
    public string Message { get; set; }
    public string Description { get; set; }
    public Dictionary<string, List<string>> FieldErrors { get; set; }
  }
}