//
// TrusonaficationResult.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public sealed class TrusonaficationResult
  {
    /// <summary>
    /// Gets the trusonafication id of the authentication request that was created.
    /// </summary>
    /// <value>The trusonafication id.</value>
    public Guid Id
    {
      get;
      internal set;
    }

    /// <summary>
    /// Gets the status of the authentication request.
    /// The status can be checked if more information is needed than the true/false response from calling the
    /// IsSuccessful method
    /// </summary>
    /// <value>The trusonafication status.</value>
    public TrusonaficationStatus Status
    {
      get;
      internal set;
    }

    /// <summary>
    /// The identifier of the user that responded to the authentication request. May be populated even
    /// if the user didn't meet all the security requirements, so it is important check the result of the
    /// result method before granting access to the user.
    /// </summary>
    /// <value>The user identifier.</value>
    [Obsolete("UserIdentifier is deprecated, use the BoundUserIdentifier field.")]
    public string UserIdentifier
    {
      get;
      internal set;
    }

    /// <summary>
    /// The identifier of the user that accepted the authentication request. Will not be populated if the
    /// user rejects or fails to complete the authentication challenge.
    /// </summary>
    /// <value>The user identifier that was previously registered, or <c>null</c> if the user has not
    /// been registered.</value>
    /// <summary>
    public string BoundUserIdentifier
    {
      get;
      internal set;
    }

    /// Returns true if the user met or exceeded the security requirements of the authentication request.
    /// Otherwise, returns false.
    /// </summary>
    /// <value><c>true</c> if is successful; otherwise, <c>false</c>.</value>
    public Boolean IsSuccessful
    {
      get
      {
        switch (Status)
        {
          case TrusonaficationStatus.ACCEPTED:
          case TrusonaficationStatus.ACCEPTED_AT_HIGHER_LEVEL:
            return true;
          default:
            return false;
        }
      }
    }
  }
}