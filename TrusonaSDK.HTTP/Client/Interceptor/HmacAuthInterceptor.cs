//
// HmacAuthInterceptor.cs
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