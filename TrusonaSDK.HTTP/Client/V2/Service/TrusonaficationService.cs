//
// TrusonaficationService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//       Nikolas Mangu-Thitu <n@trusona.com>
//
// Copyright (c) 2018-2020 Trusona, Inc.
//
using System;
using System.Threading.Tasks;

using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public sealed class TrusonaficationService : HttpService, ITrusonaficationService
  {
    private readonly ICredentialProvider credentialProvider;
    private const string Endpoint = "/api/v2/trusonafications";

    public TrusonaficationService(IConfiguration configuration, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, configuration.EndpointUrl) => credentialProvider = configuration.CredentialProvider;

    public void CancelTrusonafication(Guid id) => BlockAsyncForResult(CancelTrusonaficationAsync(id));

    public Task CancelTrusonaficationAsync(Guid id) => Delete(id: id.ToString(), resource: Endpoint, credentialProvider: credentialProvider);

    public TrusonaficationResponse CreateTrusonafication(TrusonaficationRequest request) => BlockAsyncForResult(CreateTrusonaficationAsync(request));

    public Task<TrusonaficationResponse> CreateTrusonaficationAsync(TrusonaficationRequest request) => Post<TrusonaficationResponse>(resource: Endpoint, content: request, credentialProvider: credentialProvider);

    public TrusonaficationResponse GetTrusonafication(Guid id) => BlockAsyncForResult(GetTrusonaficationAsync(id));

    public Task<TrusonaficationResponse> GetTrusonaficationAsync(Guid id) => Get<TrusonaficationResponse>(id: id.ToString(), resource: Endpoint, credentialProvider: credentialProvider);
  }
}