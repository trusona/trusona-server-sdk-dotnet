//
// VerificationStatus.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public enum VerificationStatus
  {
    /// <summary>
    /// Verification of the identity document has not been attempted.
    /// </summary>
    UNVERIFIED,

    /// <summary>
    /// Verification of the identity document was attempted but failed. 
    /// Re add the document with the Mobile SDK to retry verification.
    /// </summary>
    UNVERIFIABLE,

    /// <summary>
    /// The document was sucessfully verified.
    /// </summary>
    VERIFIED,

    /// <summary>
    /// The document failed verification.
    /// </summary>
    FAILED
  }
}
