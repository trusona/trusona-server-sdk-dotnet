//
// InterceptingHttpClientWrapper.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.Interceptor;

namespace TrusonaSDK.HTTP.Client
{
  public class InterceptingHttpClientWrapper : IHttpClientWrapper
  {
    #region Private Fields

    private static readonly HttpClient client = new HttpClient();
    private readonly IEnumerable<IHttpInterceptor> _interceptors;

    #endregion

    #region Constructors

    public InterceptingHttpClientWrapper(IEnumerable<IHttpInterceptor> interceptors)
    {
      this._interceptors = interceptors;
    }

    #endregion

    #region Public Methods

    public async Task<HttpResponseMessage> HandleRequest(HttpRequestMessage message,
                                                         ICredentialProvider credentialProvider)
    {
      foreach (var interceptor in _interceptors)
      {
        interceptor.InterceptRequest(message, credentialProvider);
      }

      var response = await client.SendAsync(message);

      foreach (var interceptor in _interceptors)
      {
        interceptor.InterceptResponse(response, credentialProvider);
      }

      return response;
    }

    #endregion
  }
}