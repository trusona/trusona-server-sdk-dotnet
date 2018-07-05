//
// DefaultHmacSignatureGenerator.cs
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
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TrusonaSDK.HTTP.Client.Security
{
  public class TrusonaHmacSignatureGenerator : IHmacSignatureGenerator, IDisposable
  {
    #region Private Fields

    private const string LF = "\n";
    private readonly HMAC hmac;

    #endregion

    #region Constructors

    public TrusonaHmacSignatureGenerator(string secret)
    {
      this.hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
    }

    #endregion

    #region Public Methods

    public string GetSignature(IHmacMessage message)
    {
      IEnumerable<string> parts = new List<string>()
      {
        message.Method,
        message.BodyDigest,
        message.ContentType,
        message.Date,
        message.RequestUri
      };

      var valueToDigest = String.Join(LF, parts);
      using (var input = ReadToStream(valueToDigest))
      {
        return Encode(hmac.ComputeHash(input));
      }
    }

    public void Dispose()
    {
      this.hmac.Dispose();
    }

    #endregion

    #region Private Methods

    private static Stream ReadToStream(string input)
    {
      return new MemoryStream(Encoding.UTF8.GetBytes(input));
    }

    private static String Encode(byte[] input)
    {
      var hexInput = BitConverter.ToString(input).Replace("-", "").ToLower();
      return Convert.ToBase64String(Encoding.ASCII.GetBytes(hexInput));
    }

    #endregion
  }
}