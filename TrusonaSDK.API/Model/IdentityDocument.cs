//
// IdentityDocument.cs
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