//
// TrusonaficationStatus.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
namespace TrusonaSDK.API.Model
{
  public enum TrusonaficationStatus
  {
    /// <summary>
    /// The authentication request could not be processed and the user was never issued a challenge.
    /// </summary>
    INVALID,

    /// <summary>
    /// The authentication request has not been responded to and has not expired.
    /// </summary>
    IN_PROGRESS,

    /// <summary>
    /// The authentication request was rejected by the user.
    /// </summary>
    REJECTED,

    /// <summary>
    /// The user accepted the authentication request and exactly met all the security requirements.
    /// </summary>
    ACCEPTED,

    /// <summary>
    /// The user accepted the authentication request, but at least one of the security requirements that were ask for
    /// was not met by the user.
    /// </summary>
    ACCEPTED_AT_LOWER_LEVEL,

    /// <summary>
    /// The user accepted the authentication request and provided more security measures than were required.
    /// </summary>
    ACCEPTED_AT_HIGHER_LEVEL,

    /// <summary>
    /// The authentication request was not responded to or timed-out before getting a response.
    /// </summary>
    EXPIRED
  }
}
