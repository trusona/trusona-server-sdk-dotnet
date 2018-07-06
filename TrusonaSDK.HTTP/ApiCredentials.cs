//
// ApiCredentials.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP
{
  public class ApiCredentials : ICredentialProvider
  {
    #region Private Fields

    private readonly string _token;
    private readonly string _secret;

    #endregion

    #region Public Properties

    public string Token
    {
      get { return _token; }
    }

    public string Secret
    {
      get { return _secret; }
    }

    #endregion

    #region Constructors

    public ApiCredentials(string token, string secret)
    {
      this._token = token;
      this._secret = secret;
    }

    #endregion
  }
}