//
// IHttpRequestInterceptor.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Net.Http;

namespace TrusonaSDK.HTTP.Client.Interceptor
{
  public interface IHttpInterceptor
  {
    void InterceptRequest(HttpRequestMessage message, ICredentialProvider credentialProvider);

    void InterceptResponse(HttpResponseMessage message, ICredentialProvider credentialProvider);
  }
}
