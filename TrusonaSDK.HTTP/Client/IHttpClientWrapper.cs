//
// IHttpClientWrapper.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrusonaSDK.HTTP.Client
{
  public interface IHttpClientWrapper
  {
    Task<HttpResponseMessage> HandleRequest(HttpRequestMessage message, ICredentialProvider credentialProvider);
  }
}
