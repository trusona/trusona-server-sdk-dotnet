//
// DeviceAlreadyBoundException.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API
{
  public class DeviceAlreadyBoundException : TrusonaException
  {
    public DeviceAlreadyBoundException(string message) : base(message)
    {
    }
  }
}
