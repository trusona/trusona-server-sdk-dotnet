//
// Device.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  /// <summary>
  /// Device.
  /// </summary>
  public sealed class Device
  {
    /// <summary>
    /// Gets the activated at timestap for the device.
    /// </summary>
    /// <value>The activated at timestamp.</value>
    public DateTime? ActivatedAt
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:TrusonaSDK.API.Model.Device"/> is active.
    /// </summary>
    /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
    public bool Active
    {
      get;
      internal set;
    }
  }
}