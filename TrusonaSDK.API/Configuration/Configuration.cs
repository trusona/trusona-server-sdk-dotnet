//
// Configuration.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2018 
//
//
using System;
using TrusonaSDK.HTTP;

namespace TrusonaSDK.API.Configuration
{
  public class Configuration : IConfiguration
  {
    #region Private Fields

    private readonly Uri _endpointUrl;
    private readonly ICredentialProvider _credentialProvider;

    #endregion

    #region Constructors

    public Configuration(string endpoint, string token, string secret)
      : this(new Uri(endpoint), new ApiCredentials(token, secret))
    { }

    protected Configuration(Uri endpointUrl, ICredentialProvider credentialProvider)
    {
      this._endpointUrl = endpointUrl;
      this._credentialProvider = credentialProvider;
    }

    #endregion

    #region Public Properties

    public virtual Uri EndpointUrl
    {
      get { return _endpointUrl; }
    }

    public ICredentialProvider CredentialProvider
    {
      get { return _credentialProvider; }
    }

    #endregion
  }
}