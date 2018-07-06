//
// UserDevice.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public class UserDevice
  {
    /// <summary>
    /// The identifier for this device.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets the unique identifier for the device. This will be the same value provided to createUserDevice
    /// </summary>
    /// <value>The device identifier.</value>
    public string DeviceIdentifier
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets the identifier for the user. This will be the same value provided to createUserDevice.
    /// </summary>
    /// <value>The user identifier.</value>
    public string UserIdentifier
    {
      get;
      internal set;
    }

    /// <summary>
    /// Whether or not the binding is active. An active binding can be used to complete Trusonafications.
    /// </summary>
    /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
    public bool Active
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets the activation code for this binding. Use this activation code to activate the binding, allowing the user to
    /// perform Trusonafications.
    /// </summary>
    /// <value>The activation code.</value>
    public string ActivationCode
    {
      get
      {
        return Active ? null : Id.ToString();
      }
    }
  }
}
