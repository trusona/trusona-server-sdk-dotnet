//
// HmacAuthInterceptor.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using TrusonaSDK.HTTP.Client.Security;
using System.Linq;

namespace TrusonaSDK.HTTP.Client.Interceptor
{
  public class HmacAuthInterceptor : IHttpInterceptor
  {
    #region Private Fields

    private const string scheme = "TRUSONA";
    private const string delimiter = ":";
    private const string rfcHttpDateFormat = "r";

    #endregion

    #region Constructors

    public HmacAuthInterceptor() { }

    #endregion

    #region Public Methods

    public void InterceptRequest(HttpRequestMessage message, ICredentialProvider credentialProvider)
    {
      if (credentialProvider == null) return;
      if (string.IsNullOrEmpty(credentialProvider.Secret)) return;

      var utcTimestamp = DateTime.Now
                                 .ToUniversalTime()
                                 .ToString(rfcHttpDateFormat);

      message.Headers.Add(Headers.X_DATE, utcTimestamp);


      using (var generator = new TrusonaHmacSignatureGenerator(credentialProvider.Secret))
      {
        string signature = generator.GetSignature(new RequestHmacMessage(message));
        message.Headers.Authorization = new AuthenticationHeaderValue(
          scheme: scheme,
          parameter: GenerateHeaderParameter(credentialProvider.Token, signature));
      }
    }

    public void InterceptResponse(HttpResponseMessage message, ICredentialProvider credentialProvider)
    {
      if (credentialProvider == null) return;
      if (string.IsNullOrEmpty(credentialProvider.Secret)) return;
      if (!message.IsSuccessStatusCode) { return; }

      IEnumerable<string> headerValues;
      if (message.Headers.TryGetValues(Headers.X_SIGNATURE, out headerValues))
      {
        var actualSignature = headerValues.First();
        using (var generator = new TrusonaHmacSignatureGenerator(credentialProvider.Secret))
        {
          var expectedSignature = generator.GetSignature(new ResponseHmacMessage(message));
          if (expectedSignature.Equals(actualSignature)) return;
        }
      }

      throw new HmacSignatureException();
    }

    #endregion

    #region Private Methods

    private static string GenerateHeaderParameter(string token, string signature)
    {
      string[] parts = { token, signature };
      return String.Join(delimiter, parts);
    }

    #endregion
  }
}