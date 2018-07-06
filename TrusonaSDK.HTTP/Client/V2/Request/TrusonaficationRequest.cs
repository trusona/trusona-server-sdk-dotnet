//
// TrusonaficationRequest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class TrusonaficationRequest : BaseRequestResponse
  {
    public string DeviceIdentifier { get; set; }
    public string UserIdentifier { get; set; }
    [JsonProperty("trucode_id")]
    public string TruCodeId { get; set; }
    public string Email { get; set; }
    public int DesiredLevel { get; set; }
    public string Action { get; set; }
    public string Resource { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string CallbackUrl { get; set; }
    public bool UserPresence { get; set; }
    public bool Prompt { get; set; }
    public bool ShowIdentityDocument { get; set; }

    public TrusonaficationRequest() { }
  }
}