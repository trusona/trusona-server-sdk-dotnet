//
// IWebSdkConfigService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IWebSdkConfigService
  {
    string GetWebSdkConfig();
  }
}
