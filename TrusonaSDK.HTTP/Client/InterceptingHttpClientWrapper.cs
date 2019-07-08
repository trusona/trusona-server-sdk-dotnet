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
using System.Security.Authentication;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.Interceptor;

namespace TrusonaSDK.HTTP.Client
{
  public class InterceptingHttpClientWrapper : IHttpClientWrapper
  {
    private readonly HttpClient client;
    private readonly IEnumerable<IHttpInterceptor> _interceptors;

    public InterceptingHttpClientWrapper(IEnumerable<IHttpInterceptor> interceptors)
    {
      this.client = new HttpClient(
        handler: new HttpClientHandler { SslProtocols = SslProtocols.Tls12 },
        disposeHandler: true
      );
      this._interceptors = interceptors;
    }

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
  }
}