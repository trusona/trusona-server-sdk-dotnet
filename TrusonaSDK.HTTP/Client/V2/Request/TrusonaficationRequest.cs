//
// TrusonaficationRequest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//       Nikolas Mangu-Thitu <n@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class TrusonaficationRequest : BaseRequestResponse
  {
    public string DeviceIdentifier { get; set; }
    
    public string UserIdentifier { get; set; }
    
    [JsonProperty("trucode_id")]
    public string TruCodeId { get; set; }
    
    [JsonProperty("email")]
    public string EmailAddress { get; set; }
    
    public int DesiredLevel { get; set; }
    
    public string Action { get; set; }
    
    public string Resource { get; set; }
    
    public DateTime? ExpiresAt { get; set; }
    
    public bool UserPresence { get; set; }
    
    public bool Prompt { get; set; }
    
    public bool ShowIdentityDocument { get; set; }
    
    public Dictionary<string, object> CustomFields { get; set; }
    
    public string CallbackUrl { get; set; }

    public TrusonaficationRequest() { }
  }
}