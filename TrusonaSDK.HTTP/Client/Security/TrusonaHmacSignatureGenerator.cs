//
// DefaultHmacSignatureGenerator.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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

    public TrusonaHmacSignatureGenerator(string secret) => hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

    #endregion

    #region Public Methods

    public string GetSignature(IHmacMessage message)
    {
      var valueToDigest = string.Join(LF, GetHmacParts(message));

      using (var input = ReadToStream(valueToDigest))
      {
        return Encode(hmac.ComputeHash(input));
      }
    }

    internal IEnumerable<string> GetHmacParts(IHmacMessage message) => new List<string>()
       {
        message.Method,
        message.BodyDigest,
        message.ContentType,
        message.Date,
        message.RequestUri
      };

    public void Dispose() => hmac.Dispose();

    #endregion

    #region Private Methods

    private static Stream ReadToStream(string input) => new MemoryStream(Encoding.UTF8.GetBytes(input));

    private static string Encode(byte[] input)
    {
      var hexInput = BitConverter.ToString(input).Replace("-", "").ToLower();
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(hexInput));
    }

    #endregion
  }
}