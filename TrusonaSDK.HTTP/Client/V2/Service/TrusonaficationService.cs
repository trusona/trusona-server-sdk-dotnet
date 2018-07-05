//
// TrusonaficationService.cs
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
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class TrusonaficationService : HttpService, ITrusonaficationService
  {
    private readonly ICredentialProvider _credentialProvider;

    public TrusonaficationService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, environment.EndpointUrl)
    {
      this._credentialProvider = environment.CredentialProvider;
    }

    public TrusonaficationResponse CreateTrusonafication(TrusonaficationRequest request)
    {
      return BlockAsyncForResult(
        CreateTrusonaficationAsync(request)
      );
    }

    public Task<TrusonaficationResponse> CreateTrusonaficationAsync(TrusonaficationRequest request)
    {
      return Post<TrusonaficationResponse>(
        resource: "/api/v2/trusonafications",
        content: request,
        credentialProvider: _credentialProvider
      );
    }

    public TrusonaficationResponse GetTrusonafication(Guid id)
    {
      return BlockAsyncForResult(
        GetTrusonaficationAsync(id)
      );
    }

    public Task<TrusonaficationResponse> GetTrusonaficationAsync(Guid id)
    {
      return Get<TrusonaficationResponse>(
        id: id.ToString(),
        resource: "/api/v2/trusonafications",
        credentialProvider: _credentialProvider
      );
    }
  }
}