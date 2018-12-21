//
// WebSdkConfigService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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
    private readonly Configuration _environment;

    public WebSdkConfigService(Configuration environment, IHttpClientWrapper clientWrapper)
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
