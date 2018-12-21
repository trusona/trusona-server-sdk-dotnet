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
using TrusonaSDK.HTTP.Environment;

namespace TrusonaSDK.API.Configuration
{
  public class ConfigurationFactory
  {
    public IConfiguration GetConfiguration(TrusonaEnvironment environment, string token, string secret)
    {
      switch (environment)
      {
        case TrusonaEnvironment.UAT:
          return new UATEnvironment(token, secret);

        case TrusonaEnvironment.PRODUCTION:
        default:
          return new ProductionEnvironment(token, secret);
      }
    }
  }
}