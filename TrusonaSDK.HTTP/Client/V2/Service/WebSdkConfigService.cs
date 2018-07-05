//
// WebSdkConfigService.cs
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
using TrusonaSDK.HTTP.Client.Security;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class WebSdkConfigService : HttpService, IWebSdkConfigService
  {
    private readonly ICredentialProvider _credentialProvider;
    private readonly IAuthTokenParser _authTokenParser;
    private readonly IEnvironment _environment;

    public WebSdkConfigService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, environment.EndpointUrl)
    {
      this._credentialProvider = environment.CredentialProvider;
      this._authTokenParser = new DefaultAuthTokenParser();
      this._environment = environment;
    }

    public string GetWebSdkConfig()
    {
      var parsedToken = _authTokenParser.ParseToken(_credentialProvider.Token);
      if (parsedToken == null)
      {
        throw new Exception("The provided token is invalid.");
      }

      IDictionary<string, object> configParams = new Dictionary<string, object>()
      {
        { "truCodeUrl", _environment.EndpointUrl },
        { "relyingPartyId", parsedToken.Subject }
      };

      return RequestResponseJsonConverter.Serialize(configParams)
                                         .Replace(System.Environment.NewLine, string.Empty)
                                         .Replace("  ", "");
    }
  }
}
