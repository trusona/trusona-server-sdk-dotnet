//
// ParsedToken.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Newtonsoft.Json;
namespace TrusonaSDK.HTTP.Client.Security
{
  public class ParsedToken
  {
    [JsonProperty("jti")]
    public Guid Id { get; set; }

    [JsonProperty("sub")]
    public Guid Subject { get; set; }

    [JsonProperty("iss")]
    public string Issuer { get; set; }

    [JsonProperty("aud")]
    public string Audience { get; set; }

    [JsonProperty("ath")]
    public string Authorities { get; set; }

    [JsonProperty("exp")]
    public int ExpiresAt { get; set; }

    [JsonProperty("iat")]
    public int IssuedAt { get; set; }
  }
}
