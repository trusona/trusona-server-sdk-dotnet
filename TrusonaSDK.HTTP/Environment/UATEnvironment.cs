//
// UATEnvironment.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Environment
{
  public class UATEnvironment : ProductionEnvironment, IEnvironment
  {
    private static readonly Uri uatEndpointUrl = new Uri("https://api.staging.trusona.net");

    public UATEnvironment(string token, string secret)
      : base(new ApiCredentials(token, secret))
    { }

    public override Uri EndpointUrl
    {
      get { return uatEndpointUrl; }
    }
  }
}
