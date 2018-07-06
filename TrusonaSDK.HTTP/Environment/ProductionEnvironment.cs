//
// Production.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Environment
{
  public class ProductionEnvironment : IEnvironment
  {
    #region Private Fields

    private static readonly Uri endpointUrl = new Uri("https://api.trusona.net");
    private readonly ICredentialProvider _credentialProvider;

    #endregion

    #region Constructors

    public ProductionEnvironment(string token, string secret)
      : this(new ApiCredentials(token, secret))
    { }

    protected ProductionEnvironment(ICredentialProvider credentialProvider)
    {
      this._credentialProvider = credentialProvider;
    }

    #endregion

    #region Public Properties

    public virtual Uri EndpointUrl
    {
      get { return endpointUrl; }
    }

    public ICredentialProvider CredentialProvider
    {
      get { return _credentialProvider; }
    }

    #endregion
  }
}
