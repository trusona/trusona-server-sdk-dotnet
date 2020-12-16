//
// HmacAuthInterceptor.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using TrusonaSDK.HTTP.Client.Security;

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
      if (credentialProvider == null || !credentialProvider.HasCredentials) { return; }

      var utcTimestamp = DateTime.Now.ToUniversalTime().ToString(rfcHttpDateFormat);

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
      if (!message.IsSuccessStatusCode) { return; }

      if (message.Headers.TryGetValues(Headers.X_SIGNATURE, out IEnumerable<string> headerValues))
      {
        string actualSignature, hmacParts, expectedSignature = "";

        if (credentialProvider?.HasCredentials == true)
        {
          actualSignature = headerValues.First();
        }
        else
        {
          throw new HmacSignatureException("Credentials not available for HMAC Signature verification");
        }

        using (var generator = new TrusonaHmacSignatureGenerator(credentialProvider.Secret))
        {
          hmacParts = string.Join("\n", generator.GetHmacParts(new ResponseHmacMessage(message))); // debugging

          expectedSignature = generator.GetSignature(new ResponseHmacMessage(message));
          if (expectedSignature.Equals(actualSignature)) { return; }
        }

        throw new HmacSignatureException(string.Format("Failed to verify response HMAC after {0}.\nExpected: {1} vs. actual: {2}\n{3}", message.StatusCode, expectedSignature, actualSignature, hmacParts));
      }
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