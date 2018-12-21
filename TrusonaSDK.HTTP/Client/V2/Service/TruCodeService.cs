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
    private readonly IConfiguration _configuration;
    private readonly IHttpClientWrapper _clientWrapper;
    private readonly ICredentialProvider _credentialProvider;

    public TruCodeService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : this(configuration, clientWrapper, configuration.EndpointUrl)
    { }

    private TruCodeService(IConfiguration configuration, IHttpClientWrapper clientWrapper, Uri endpointUri)
      : base(new RequestResponseJsonConverter(), clientWrapper, endpointUri)
    {
      this._configuration = configuration;
      this._clientWrapper = clientWrapper;
      this._credentialProvider = configuration.CredentialProvider;
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