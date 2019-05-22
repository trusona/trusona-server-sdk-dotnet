//
// IntegrationServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using TrusonaSDK.API;
using TrusonaSDK.Test;

namespace TrusonaSDK
{
  public abstract class IntegrationTest
  {
    protected readonly Trusona sut;
    protected readonly Buster buster;

    protected IntegrationTest()
    {
      var environment = System.Environment.GetEnvironmentVariables();
      sut = new TrusonaSDK.API.Trusona(
        token: (string)environment["TRUSONA_TOKEN"],
        secret: (string)environment["TRUSONA_SECRET"],
        environment: TrusonaEnvironment.UAT
      );
      buster = new Buster();
    }
  }
}