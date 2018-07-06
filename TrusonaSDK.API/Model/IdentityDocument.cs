//
// IdentityDocument.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public class IdentityDocument
  {
    /// <summary>
    /// The Identity Document ID.
    /// </summary>
    /// <value>The ID.</value>
    public Guid Id
    {
      get;
      internal set;
    }

    /// <summary>
    /// The hash of the identity docment.
    /// </summary>
    /// <value>The hash.</value>
    public string Hash
    {
      get;
      internal set;
    }

    /// <summary>
    /// The verification status of the identity document.
    /// </summary>
    /// <value>The verification status.</value>
    public VerificationStatus VerificationStatus
    {
      get;
      internal set;
    }

    /// <summary>
    /// The timestamp of when the document verificaiton was successfully verified. 
    /// </summary>
    /// <value>The verified at.</value>
    public DateTime? VerifiedAt
    {
      get;
      internal set;
    }

    /// <summary>
    /// The type of identity document. 
    /// </summary>
    /// <value>The type.</value>
    public string Type
    {
      get;
      internal set;
    }
  }
}