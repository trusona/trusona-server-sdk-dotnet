//
// TruCodeResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class TruCodeResponse : BaseRequestResponse
  {
    public Guid Id { get; set; }
    public string Identifier { get; set; }
  }
}