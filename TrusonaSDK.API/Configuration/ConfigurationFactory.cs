//
// ConfigurationFactory.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2018 
//
//
using System.Collections.Generic;
using TrusonaSDK.HTTP;

namespace TrusonaSDK.API.Configuration
{
  public class ConfigurationFactory
  {
    private static readonly Dictionary<TrusonaEnvironment, string> endpoints = new Dictionary<TrusonaEnvironment, string>()
    {
      { TrusonaEnvironment.PRODUCTION, "https://api.trusona.net" },
      { TrusonaEnvironment.UAT, "https://api.staging.trusona.net"},
      { TrusonaEnvironment.AP_PRODUCTION, "https://api.ap.trusona.net/"},
      { TrusonaEnvironment.AP_UAT, "https://api.staging.ap.trusona.net/"}
    };

    public IConfiguration GetConfiguration(TrusonaEnvironment environment, string token, string secret)
    {
      var endpoint = endpoints[environment];

      return new Configuration(endpoint, token, secret);
    }
  }
}