//
// ConfigurationFactory.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2018 
//
//
using TrusonaSDK.HTTP;

namespace TrusonaSDK.API.Configuration
{
  public class ConfigurationFactory
  {
    private static readonly string PRODUCTION_ENDPOINT = "https://api.trusona.net";
    private static readonly string UAT_ENDPOINT = "https://api.staging.trusona.net";
    private static readonly string AP_PRODUCTION_ENDPOINT = "https://api.ap.trusona.net/";

    public IConfiguration GetConfiguration(TrusonaEnvironment environment, string token, string secret)
    {
      switch (environment)
      {
        case TrusonaEnvironment.UAT:
          return new Configuration(UAT_ENDPOINT, token, secret);

        case TrusonaEnvironment.AP_PRODUCTION:
          return new Configuration(AP_PRODUCTION_ENDPOINT, token, secret);

        case TrusonaEnvironment.PRODUCTION:
        default:
          return new Configuration(PRODUCTION_ENDPOINT, token, secret);
      }
    }
  }
}