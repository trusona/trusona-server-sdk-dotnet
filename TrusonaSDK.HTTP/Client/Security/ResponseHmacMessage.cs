//
// ResponseHmacMessage.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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

    public ResponseHmacMessage(HttpResponseMessage message) => _message = message;

    #endregion

    #region Private Methods

    private static string GetRequestUri(Uri requestUri) => requestUri?.PathAndQuery;

    private static string GetContentType(HttpContent content) => content?.Headers?.ContentType?.ToString() ?? string.Empty;

    private static string GetHeaderValue(HttpResponseHeaders headers, string name) => headers.TryGetValues(name, out IEnumerable<string> headerValues) ? headerValues?.First() : null;

    private static string Md5Digest(HttpContent httpContent)
    {
      string valueToDigest = httpContent?.ReadAsStringAsync()?.Result ?? string.Empty;
      byte[] digest;

      using (var md5 = MD5.Create())
      {
        using (var inputStream = ReadToStream(valueToDigest))
        {
          digest = md5.ComputeHash(inputStream);
        }
      }

      return HexEncode(digest);
    }

    private static Stream ReadToStream(string input) => new MemoryStream(Encoding.UTF8.GetBytes(input));

    private static string HexEncode(byte[] input) => BitConverter.ToString(input).Replace("-", "").ToLower();

    #endregion
  }
}