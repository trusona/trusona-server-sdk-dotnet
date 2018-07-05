//
// ServiceFactory.cs
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