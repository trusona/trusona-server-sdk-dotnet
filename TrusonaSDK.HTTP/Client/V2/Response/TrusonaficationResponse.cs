//
// TrusonaficationResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;

using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class TrusonaficationResponse : TrusonaficationRequest
  {
    public Guid Id { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string TrusonaId { get; set; }

    public TrusonaficationResultResponse Result { get; set; }
  }
}