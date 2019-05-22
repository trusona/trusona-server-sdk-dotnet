//
// UserBindingRequest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class UserBindingRequest : BaseRequestResponse
  {
    [JsonProperty("trucode_id")]
    public string TruCodeId { get; set; }
    public string UserIdentifier { get; set; }

    public UserBindingRequest() { }
  }
}
