//
// WebSdkApiExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using static TrusonaSDK.API.Trusona;
namespace TrusonaSDK.API
{
  /// <summary>
  /// Web SDK API.
  /// </summary>
  public static class WebSdkExtension
  {
    /// <summary>
    /// Gets the web sdk config.
    /// </summary>
    /// <returns>The web sdk config.</returns>
    /// <param name="trusona">Trusona API.</param>
    public static string GetWebSdkConfig(this Trusona trusona)
    {
      return trusona.WebSdkConfigService.GetWebSdkConfig();
    }

  }
}