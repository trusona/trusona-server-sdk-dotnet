//
// TrusonaficationService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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