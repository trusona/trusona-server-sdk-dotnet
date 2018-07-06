//
// ServiceFactory.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using TrusonaSDK.HTTP.Client.Interceptor;
using TrusonaSDK.HTTP.Client.V2.Service;

namespace TrusonaSDK.HTTP.Client
{
  public sealed class ServiceFactory
  {
    #region Static Fields

    private static readonly IHttpClientWrapper defaultClientWrapper =
      new InterceptingHttpClientWrapper(new List<IHttpInterceptor>()
      {
        new HmacAuthInterceptor()
      });

    #endregion

    #region Private Fields

    private readonly IEnvironment _environment;
    private readonly IHttpClientWrapper _clientWrapper;

    #endregion

    #region Constructors

    public ServiceFactory(IEnvironment environment)
      : this(environment, defaultClientWrapper)
    { }

    public ServiceFactory(IEnvironment environment, IHttpClientWrapper clientWrapper)
    {
      this._environment = environment;
      this._clientWrapper = clientWrapper;
    }

    #endregion

    #region Public Methods

    public T CreateInstance<T>() where T : HttpService
    {
      return CreateInstance<T>(_clientWrapper);
    }

    internal T CreateInstance<T>(IHttpClientWrapper clientWrapper)
    {
      return (T)Activator.CreateInstance(
        typeof(T),
        _environment,
        clientWrapper);
    }

    #endregion
  }
}