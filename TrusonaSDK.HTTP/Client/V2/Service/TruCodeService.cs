//
// TruCodeServiceHttpClient.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class TruCodeService : HttpService, ITruCodeService
  {
    private readonly IEnvironment _environment;
    private readonly IHttpClientWrapper _clientWrapper;
    private readonly ICredentialProvider _credentialProvider;

    public TruCodeService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : this(environment, clientWrapper, environment.EndpointUrl)
    { }

    private TruCodeService(IEnvironment environment, IHttpClientWrapper clientWrapper, Uri endpointUri)
      : base(new RequestResponseJsonConverter(), clientWrapper, endpointUri)
    {
      this._environment = environment;
      this._clientWrapper = clientWrapper;
      this._credentialProvider = environment.CredentialProvider;
    }

    public TruCodeResponse GetPairedTrucode(Guid trucodeId)
    {
      return BlockAsyncForResult(
        GetPairedTrucodeAsync(trucodeId)
      );
    }

    public Task<TruCodeResponse> GetPairedTrucodeAsync(Guid trucodeId)
    {
      return Get<TruCodeResponse>(
        id: trucodeId.ToString(),
        resource: "/api/v2/paired_trucodes",
        credentialProvider: _credentialProvider
      );
    }
  }
}