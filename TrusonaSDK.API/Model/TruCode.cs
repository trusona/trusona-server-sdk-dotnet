//
// TruCode.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public class TruCode
  {
    /// <summary>
    /// Gets or sets ID of the TruCode.
    /// </summary>
    /// <value>The ID of the TruCode.</value>
    public Guid Id
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets or sets the identifier that was paired to this TruCode.
    /// </summary>
    /// <value>The identifier that was paired to this TruCode.</value>
    public string Identifier
    {
      get;
      internal set;
    }
  }
}
