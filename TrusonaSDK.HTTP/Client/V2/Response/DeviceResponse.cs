//
// DeviceResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class DeviceResponse : BaseRequestResponse
  {
    public DateTime? ActivatedAt { get; set; }
    [JsonProperty("is_active")]
    public bool Active { get; set; }
  }
}