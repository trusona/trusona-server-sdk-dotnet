//
// ResponseHmacMessage.cs
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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace TrusonaSDK.HTTP.Client.Security
{
  public class ResponseHmacMessage : IHmacMessage
  {
    #region Private Fields

    private readonly HttpResponseMessage _message;

    #endregion

    #region Public Properties

    public string BodyDigest
    {
      get
      {
        return Md5Digest(_message.Content);
      }
    }

    public string ContentType
    {
      get
      {
        return GetContentType(_message.Content);
      }
    }

    public string Date
    {
      get
      {
        return GetHeaderValue(_message.Headers, Headers.X_DATE);
      }
    }

    public string Method
    {
      get
      {
        if (_message.RequestMessage == null) return null;
        return _message.RequestMessage.Method
                    .ToString();
      }
    }

    public string RequestUri
    {
      get
      {
        if (_message.RequestMessage == null) return null;
        return GetRequestUri(_message.RequestMessage.RequestUri);
      }
    }

    #endregion

    #region Constructors

    public ResponseHmacMessage(HttpResponseMessage message)
    {
      this._message = message;
    }

    #endregion

    #region Private Methods

    private static string GetRequestUri(Uri requestUri)
    {
      if (requestUri == null)
      {
        return null;
      }
      return requestUri.PathAndQuery;
    }

    private static string GetContentType(HttpContent content)
    {
      if (content == null)
      {
        return null;
      }

      var contentTypeHeader = content.Headers.ContentType;
      var sb = new StringBuilder();

      sb.Append(contentTypeHeader.MediaType);

      foreach (var param in contentTypeHeader.Parameters)
      {
        sb.Append(";");
        sb.Append(param);
      }
      return sb.ToString();
    }

    private static string GetHeaderValue(HttpResponseHeaders headers, string name)
    {
      IEnumerable<string> headerValues;
      if (!headers.TryGetValues(name, out headerValues)) return null;
      return headerValues.First();
    }

    private static string Md5Digest(HttpContent httpContent)
    {
      string valueToDigest = string.Empty;
      if (httpContent != null)
      {
        valueToDigest = httpContent.ReadAsStringAsync().Result;
      }

      byte[] digest;
      using (var md5 = MD5.Create())
      using (var inputStream = ReadToStream(valueToDigest))
      {
        digest = md5.ComputeHash(inputStream);
      }

      return HexEncode(digest);
    }

    private static Stream ReadToStream(string input)
    {
      return new MemoryStream(Encoding.UTF8.GetBytes(input));
    }

    private static String HexEncode(byte[] input)
    {
      return BitConverter.ToString(input).Replace("-", "").ToLower();
    }

    #endregion
  }
}