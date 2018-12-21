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
    private readonly IConfiguration _configuration;

    public WebSdkConfigService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl)
    {
      this._credentialProvider = configuration.CredentialProvider;
      this._authTokenParser = new DefaultAuthTokenParser();
      this._configuration = configuration;
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
        { "truCodeUrl", _configuration.EndpointUrl },
        { "relyingPartyId", parsedToken.Subject }
      };

      return RequestResponseJsonConverter.Serialize(configParams)
                                         .Replace(System.Environment.NewLine, string.Empty)
                                         .Replace("  ", "");
    }
  }
}
