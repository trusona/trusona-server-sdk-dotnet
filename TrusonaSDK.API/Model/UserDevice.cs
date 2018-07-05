//
// UserDevice.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
