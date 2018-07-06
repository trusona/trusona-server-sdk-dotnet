//
// TestableTrusonaFactory.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client;

namespace TrusonaSDK.API.Test
{
  public static class TestTrusonaFactory
  {
    public static Trusona InjectServiceFactory(ServiceFactory serviceFactory)
    {
      return new Trusona(serviceFactory);
    }

  }
}
