//
// DeviceNotFoundException.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API
{
  public class DeviceNotFoundException : TrusonaException
  {
    public DeviceNotFoundException(string message) : base(message)
    {
    }
  }
}
