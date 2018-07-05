//
// TrusonaficationStatus.cs
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
