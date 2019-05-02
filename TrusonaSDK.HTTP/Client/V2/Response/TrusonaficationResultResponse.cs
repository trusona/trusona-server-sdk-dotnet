//
// TrusonaficationResultResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class TrusonaficationResultResponse : BaseRequestResponse
  {
    public Guid Id { get; set; }
    [JsonProperty("is_accepted")]
    public bool Accepted { get; set; }
    public int AcceptedLevel { get; set; }
    public String BoundUserIdentifier { get; set; }
  }
}
