//
// IntegrationServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Linq;
using TrusonaSDK.API;

namespace TrusonaSDK
{
  public abstract class IntegrationServiceTest
  {
    protected readonly TrusonaSDK.API.Trusona sut;

    protected IntegrationServiceTest()
    {
      var environment = System.Environment.GetEnvironmentVariables();
      sut = new TrusonaSDK.API.Trusona(
        token: (string)environment["TRUSONA_TOKEN"],
        secret: (string)environment["TRUSONA_SECRET"],
        environment: TrusonaEnvironment.UAT
      );
    }
  }
}