//
// RequestHmacMessage.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace TrusonaSDK.HTTP.Client.Security
{
  public class RequestHmacMessage : IHmacMessage
  {
    #region Private Fields

    private readonly HttpRequestMessage _message;

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
        return _message.Method
                    .ToString();
      }
    }

    public string RequestUri
    {
      get
      {
        return GetRequestUri(_message.RequestUri);
      }
    }

    #endregion

    #region Constructors

    public RequestHmacMessage(HttpRequestMessage message)
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
      return content.Headers
                    .ContentType
                    .ToString();
    }

    private static string GetHeaderValue(HttpRequestHeaders headers, string name)
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
